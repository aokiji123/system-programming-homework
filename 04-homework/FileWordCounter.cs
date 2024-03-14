using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

class Program {
    static async Task Main(string[] args) {
        Console.Write("Word to search: ");
        string word = Console.ReadLine();

        // searching in macos documents system folder
        Console.Write("Name of the file in Documents folder (e.g., Hello.txt/rtf): ");
        string fileName = Console.ReadLine();

        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string filePath = Path.Combine(documentsPath, fileName);
        Console.WriteLine(filePath);

        int occurrences = await SearchWordInFileAsync(word, filePath);

        if (occurrences == -1) Console.WriteLine("File not found");
        else Console.WriteLine($"The word '{word}' occurs {occurrences} times");
    }

    static async Task < int > SearchWordInFileAsync(string word, string filePath) {
        try {
            string content = await File.ReadAllTextAsync(filePath);
            int occurrences = content.Split(new char[] {
                    ' ',
                    '\n',
                    '\r',
                    '\t'
                }, StringSplitOptions.RemoveEmptyEntries)
                .Count(w => w.Equals(word, StringComparison.OrdinalIgnoreCase));
            return occurrences;
        } catch (FileNotFoundException) {
            return -1;
        }
    }
}