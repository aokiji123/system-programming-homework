using System;
using System.IO;
using System.Threading.Tasks;

public class FileManager
{
    public async Task EditFileAsync() {
        Console.Write("Enter the name of the file to edit (with extension): ");
        string fileName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(fileName)) {
            Console.WriteLine("Invalid file name.");
            return;
        }

        string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        if (!File.Exists(path)) {
            Console.WriteLine("File does not exist.");
            return;
        }

        Console.WriteLine("Current contents of the file:");
        string existingContent = await File.ReadAllTextAsync(path);
        Console.WriteLine(existingContent);

        Console.WriteLine("Enter new content to write in the file:");
        string newContent = Console.ReadLine();

        if (newContent == null) {
            Console.WriteLine("Invalid content.");
            return;
        }

        try {
            using (StreamWriter sw = new StreamWriter(path, false)) {
                await sw.WriteAsync(newContent);
            }
            Console.WriteLine("File edited successfully.");
        } catch (Exception ex) {
            Console.WriteLine($"Error editing file: {ex.Message}.");
        }
    }

    public async Task DeleteFileAsync() {
        Console.Write("Enter the name of the file to delete (with extension): ");
        string fileName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(fileName)) {
            Console.WriteLine("Invalid file name.");
            return;
        }

        string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        if (!File.Exists(path)) {
            Console.WriteLine("File does not exist.");
            return;
        }

        try {
            File.Delete(path);
            Console.WriteLine("File deleted successfully.");
        } catch (Exception ex) {
            Console.WriteLine($"Error deleting file: {ex.Message}");
        }
    }

    public async Task CreateFileAsync() {
        Console.Write("Enter file name with extension to create: ");
        string fileName = Console.ReadLine();
        Console.Write("Enter text to write in the file: ");
        string content = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(fileName)) {
            Console.WriteLine("Invalid file name.");
            return;
        }

        if (content == null) {
            Console.WriteLine("Invalid content.");
            return;
        }

        string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        try {
            using (FileStream fs = await Task.Run(() => File.Create(path))) {
                using (StreamWriter sw = new StreamWriter(fs)) {
                    await sw.WriteLineAsync(content);
                }
            }
            Console.WriteLine("File created successfully.");
        } catch (Exception ex) {
            Console.WriteLine($"Error creating file: {ex.Message}.");
        }
    }

}