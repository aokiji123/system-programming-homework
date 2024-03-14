using ThreadedNumberGenerator;

int userChoice;
bool exitProgram = false;

Thread? primeNumberThread = null;
Thread? fibonacciNumberThread = null;

PrimeGenerator? primeNumberGenerator = null;
FibonacciGenerator? fibonacciNumberGenerator = null;

while (!exitProgram)
{
    Console.Clear();
    Console.WriteLine("Select an action:");
    Console.WriteLine("1. Start Prime Number Generator");
    Console.WriteLine("2. Start Fibonacci Sequence Generator");
    Console.WriteLine("3. Terminate Prime Number Generator");
    Console.WriteLine("4. Terminate Fibonacci Sequence Generator");
    Console.WriteLine("5. Close Application");

    Console.Write("Enter your choice: ");
    userChoice = int.Parse(Console.ReadLine());

    switch (userChoice)
    {
        case 1:
            Console.Write("Enter the maximum number for prime number generation: ");
            int upperBoundForPrimes = int.Parse(Console.ReadLine());
            primeNumberGenerator = new PrimeGenerator(upperBoundForPrimes);
            primeNumberThread = new Thread(primeNumberGenerator.GeneratePrimes);
            Console.WriteLine("Prime number generation is now active...");
            primeNumberThread.Start();
            break;

        case 2:
            Console.Write("Enter the maximum number for Fibonacci sequence generation: ");
            int upperBoundForFibonacci = int.Parse(Console.ReadLine());
            fibonacciNumberGenerator = new FibonacciGenerator(upperBoundForFibonacci);
            fibonacciNumberThread = new Thread(fibonacciNumberGenerator.GenerateFibonacci);
            Console.WriteLine("Fibonacci sequence generation is now active...");
            fibonacciNumberThread.Start();
            break;

        case 3:
            if (primeNumberThread != null)
            {
                primeNumberGenerator.Stop();
                primeNumberThread.Join();
                Console.WriteLine("Prime numbers generated:");
                foreach (int number in primeNumberGenerator.GetNumbers())
                {
                    Console.Write(number + " ");
                }
                Console.WriteLine("\nPrime number generation has been terminated.");
                Thread.Sleep(5000);
            }
            else Console.WriteLine("Prime number generator is not currently active.");
            break;

        case 4:
            if (fibonacciNumberThread != null)
            {
                fibonacciNumberGenerator.Stop();
                fibonacciNumberThread.Join();
                Console.WriteLine("Fibonacci numbers generated:");
                foreach (int number in fibonacciNumberGenerator.GetNumbers())
                {
                    Console.Write(number + " ");
                }
                Console.WriteLine("\nFibonacci sequence generation has been terminated.");
                Thread.Sleep(5000);
            }
            else Console.WriteLine("Fibonacci sequence generator is not currently active.");
            break;

        case 5:
            exitProgram = true;
            break;

        default:
            Console.WriteLine("Invalid selection. Please enter a number between 1 and 5.");
            break;
    }
}
