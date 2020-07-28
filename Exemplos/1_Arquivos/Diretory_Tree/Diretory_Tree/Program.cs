using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diretory_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\Program Files");
            ListDirectories(directoryInfo, "*a*", 5, 0);



            // Adquira o Drive
            DriveInfo d_info = new DriveInfo(@"C:\");

            //deferred execution
            var diretorios_info = d_info.RootDirectory.EnumerateDirectories();
            //immediate execution
            var diretorios_get = d_info.RootDirectory.GetDirectories();

            var stopwatch_info = new Stopwatch();

            stopwatch_info.Start();

            foreach (var dirinfo in diretorios_info)
            {
                Console.WriteLine("O nome é:" + dirinfo.Name);
            }

            stopwatch_info.Stop();
            Console.WriteLine("Deferred:" + stopwatch_info.ElapsedMilliseconds);


            var stopwatch_get = new Stopwatch();
            stopwatch_get.Start();

            Console.WriteLine("********************");

            foreach (var dirinfo in diretorios_get)
            {
                Console.WriteLine("O nome é:" + dirinfo.Name);
            }

            stopwatch_get.Stop();
            Console.WriteLine("Immediate:" + stopwatch_get.ElapsedMilliseconds);

            Console.ReadKey();
        }

        private static void ListDirectories(DirectoryInfo directoryInfo, string searchPattern, int maxLevel, int currentLevel)
        {
            if (currentLevel >= maxLevel)
            {
                return;
            }
            string indent = new string('-', currentLevel);
            try
            {
                DirectoryInfo[] subDirectories = directoryInfo.GetDirectories(searchPattern);
                foreach (DirectoryInfo subDirectory in subDirectories)
                {
                    Console.WriteLine(indent + subDirectory.Name);
                    ListDirectories(subDirectory, searchPattern, maxLevel, currentLevel + 1);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // You don’t have access to this folder.
                Console.WriteLine(indent + "Can’t access: " + directoryInfo.Name);
                return;
            }
            catch (DirectoryNotFoundException)
            {
                // The folder is removed while iterating
                Console.WriteLine(indent + "Can’t find: " + directoryInfo.Name);
                return;
            }

        }
    }
}
