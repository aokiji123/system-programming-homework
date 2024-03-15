using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        FileManager fileManager = new FileManager();
        
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Edit File");
            Console.WriteLine("2. Delete File");
            Console.WriteLine("3. Create File");
            Console.WriteLine("4. Exit");

            switch (Console.ReadLine())
            {
                case "1":
                    await fileManager.EditFileAsync();
                    break;
                case "2":
                    await fileManager.DeleteFileAsync();
                    break;
                case "3":
                    await fileManager.CreateFileAsync();
                    break;
                case "4":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }
}
