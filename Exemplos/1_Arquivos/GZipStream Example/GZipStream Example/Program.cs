using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZipStream_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            string folder = @"c:\temp";
            string uncompressedFilePath = Path.Combine(folder, "uncompressed.dat");
            string compressedFilePath = Path.Combine(folder, "compressed.gz");
            byte[] dataToCompress = Enumerable.Repeat((byte)'a', 1024 * 1024).ToArray();
            using (FileStream uncompressedFileStream = File.Create(uncompressedFilePath))
            {
                uncompressedFileStream.Write(dataToCompress, 0, dataToCompress.Length);
            }
            using (FileStream compressedFileStream = File.Create(compressedFilePath))
            {
                using (GZipStream compressionStream = new GZipStream(
                compressedFileStream, CompressionMode.Compress))
                {
                    compressionStream.Write(dataToCompress, 0, dataToCompress.Length);
                }
            }
            FileInfo uncompressedFile = new FileInfo(uncompressedFilePath);
            FileInfo compressedFile = new FileInfo(compressedFilePath);
            Console.WriteLine(uncompressedFile.Length); // Displays 1048576
            Console.WriteLine(compressedFile.Length); // Displays 1052


            string path = @"c:\temp\bufferedStream.txt";
            using (FileStream fileStream = File.Create(path))
            {
                using (BufferedStream bufferedStream = new BufferedStream(fileStream))
                {
                    using (StreamWriter streamWriter = new StreamWriter(bufferedStream))
                    {
                        streamWriter.WriteLine("A line of text.");
                    }
                }
            }





            Console.ReadKey();
        }
    }
}
