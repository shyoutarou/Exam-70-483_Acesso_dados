using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Adquira o Drive
            DriveInfo d_info = new DriveInfo(@"C:\");
            Console.WriteLine("O nome é: " + d_info.Name);
            Console.WriteLine("Tipo de unidade é: " + d_info.DriveType);
            Console.WriteLine("********************");

            // Obtenha toda a unidade
            DriveInfo[] driveInfo = DriveInfo.GetDrives();
            foreach (DriveInfo info in driveInfo)
            {
                Console.WriteLine("Drive {0}", info.Name);
                Console.WriteLine("File type: {0}", info.DriveType);
                if (info.IsReady == true)
                {
                    Console.WriteLine("Volume label: {0}", info.VolumeLabel);
                    Console.WriteLine("File system: {0}", info.DriveFormat);
                    Console.WriteLine("Available space to current user:{0,15} bytes", info.AvailableFreeSpace);
                    Console.WriteLine("Total available space: {0,15} bytes", info.TotalFreeSpace);
                    Console.WriteLine("Total size of drive: {0,15} bytes", info.TotalSize);
                }
            }

            Console.ReadKey();
        }
    }
}
