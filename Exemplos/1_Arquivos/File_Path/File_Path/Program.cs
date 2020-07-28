using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Path
{
    class Program
    {
        static void Main(string[] args)
        {
            string folder = @"C:\temp";
            string fileName = "test.dat";
            string fullPath = folder + fileName; // Results in C:\temptest.dat
            Console.WriteLine(fullPath);

            fullPath = Path.Combine(folder, fileName); // Results in C:\\temp\\test.dat
            Console.WriteLine(fullPath);

            string path = @"C:\temp\subdir\file.txt";
            Console.WriteLine(Path.GetDirectoryName(path)); // Displays C:\temp\subdir
            Console.WriteLine(Path.GetExtension(path)); // Displays .txt
            Console.WriteLine(Path.GetFileName(path)); // Displays file.txt
            Console.WriteLine(Path.GetPathRoot(path)); // Displays C:\

            Console.ReadKey();
        }
    }
}
