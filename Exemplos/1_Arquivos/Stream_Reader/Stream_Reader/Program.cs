using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stream_Reader
{
    class Program
    {
        static void Main(string[] args)
        {
            // This will get the current WORKING directory (i.e. \bin\Debug)
            string workingDirectory = Environment.CurrentDirectory;
            // or: Directory.GetCurrentDirectory() gives the same result

            // This will get the current PROJECT directory
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

            using (StreamReader streamReader = new StreamReader(projectDirectory + "\\Numbers.txt"))
            {
                Console.WriteLine("Char by Char");
                while (!streamReader.EndOfStream)
                    Console.WriteLine((char)streamReader.Read());
            }

            using (StreamReader streamReader = new StreamReader(projectDirectory + "\\Numbers.txt"))
            {
                Console.WriteLine("Line by line");
                while (!streamReader.EndOfStream)
                    Console.WriteLine(streamReader.ReadLine());
            }

            using (StreamReader streamReader = new StreamReader(projectDirectory + "\\Numbers.txt"))
            {
                Console.WriteLine("Entire file");
                Console.WriteLine(streamReader.ReadToEnd());
            }

            using (StreamWriter streamWriter = new StreamWriter(projectDirectory + "\\Numbers.txt"))
            {
                streamWriter.Write('A');
            }

            using (StreamReader streamReader = new StreamReader(projectDirectory + "\\Numbers.txt"))
            {
                Console.WriteLine(streamReader.ReadLine());
            }

            Console.ReadKey();
        }
    }
}
