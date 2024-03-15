using System;
using System.IO;
using System.Threading;

class RandomNumberProcessor
{
    static Mutex controlMutex = new Mutex();
    static string sourceFile = "numbers_data.txt";
    static string primesOutputFile = "filtered_primes.txt";
    static string primesWith7OutputFile = "primes_ending_with_7.txt";
    static string summaryReportFile = "processing_report.txt";

    static void Main(string[] args)
    {
        Thread threadRandomGen = new Thread(CreateRandomNumbers);
        Thread threadPrimeFinder = new Thread(ExtractPrimes);
        Thread threadPrimeWith7Finder = new Thread(ExtractPrimesEndingWith7);
        Thread threadReportGenerator = new Thread(CompileReport);

        threadRandomGen.Start();
        threadPrimeFinder.Start();
        threadPrimeWith7Finder.Start();
        threadReportGenerator.Start();
    }

    static void CreateRandomNumbers()
    {
        controlMutex.WaitOne();
        Random randomGen = new Random();
        using (StreamWriter fileWriter = new StreamWriter(sourceFile)) { for (int i = 0; i < 100; i++) fileWriter.WriteLine(randomGen.Next(1, 1000)); }
        controlMutex.ReleaseMutex();
    }

    static void ExtractPrimes()
    {
        controlMutex.WaitOne();
        using (StreamReader fileReader = new StreamReader(sourceFile))
        {
            using (StreamWriter fileWriter = new StreamWriter(primesOutputFile))
            {
                string numberLine;
                while ((numberLine = fileReader.ReadLine()) != null)
                {
                    int number = int.Parse(numberLine);
                    if (IsNumberPrime(number)) fileWriter.WriteLine(number);
                }
                controlMutex.ReleaseMutex();
            }
        }
    }

    static void ExtractPrimesEndingWith7()
    {
        controlMutex.WaitOne();
        using (StreamReader fileReader = new StreamReader(primesOutputFile))
        using (StreamWriter fileWriter = new StreamWriter(primesWith7OutputFile))
        {
            string numberLine;
            while ((numberLine = fileReader.ReadLine()) != null) { int number = int.Parse(numberLine); if (number % 10 == 7) fileWriter.WriteLine(number); }
        }
        controlMutex.ReleaseMutex();
    }

    static bool IsNumberPrime(int number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;
        for (int i = 3; i <= Math.Sqrt(number); i += 2) if (number % i == 0) return false;
        return true;
    }

    static void CompileReport()
    {
        controlMutex.WaitOne();
        using (StreamWriter fileWriter = new StreamWriter(summaryReportFile))
        {
            GenerateReport(fileWriter, sourceFile);
            GenerateReport(fileWriter, primesOutputFile);
            GenerateReport(fileWriter, primesWith7OutputFile);
        }
        controlMutex.ReleaseMutex();
    }

    static void GenerateReport(StreamWriter fileWriter, string filename)
    {
        fileWriter.WriteLine($"File: {filename}");
        fileWriter.WriteLine($"Number of Entries: {File.ReadLines(filename).Count()}");
        fileWriter.WriteLine($"File Size (bytes): {new FileInfo(filename).Length}");
        fileWriter.WriteLine($"Contents:");
        foreach (string line in File.ReadAllLines(filename)) fileWriter.WriteLine(line);
        fileWriter.WriteLine();
    }
}
