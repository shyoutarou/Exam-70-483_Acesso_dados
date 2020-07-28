using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsolatedStorageFile_Stream
{
    class Program
    {
        static void Main(string[] args)
        {
            //IsolatedStorageFile é essencialmente um ponteiro para 
            //o arquivo de armazenamento isolado(área) no disco.
            var isolated = IsolatedStorageFile.GetUserStoreForApplication();
            using (var writer = new StreamWriter(isolated.CreateFile("TestStore.txt")))
            {
                writer.WriteLine("Text");
            }


            //IsolatedStorageFileStream é uma representação na memória 
            //dos dados em um arquivo dentro da área de armazenamento isolada.
            var isolated_stream = IsolatedStorageFile.GetUserStoreForApplication();
            using (StreamWriter writer = new StreamWriter(new IsolatedStorageFileStream("TestStore.txt", FileMode.Create, FileAccess.Write, isolated_stream)))
            {
                writer.WriteLine("Text");
            }

            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            if (isoStore.FileExists("TestStore.txt"))
            {
                Console.WriteLine("The file already exists!");
                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("TestStore.txt", FileMode.Open, isoStore))
                {
                    using (StreamReader reader = new StreamReader(isoStream))
                    {
                        Console.WriteLine("Reading contents:");
                        Console.WriteLine(reader.ReadToEnd());
                    }
                }
            }
            else
            {
                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("TestStore.txt", FileMode.CreateNew, isoStore))
                {
                    using (StreamWriter writer = new StreamWriter(isoStream))
                    {
                        writer.WriteLine("Hello Isolated Storage");
                        Console.WriteLine("You have written to the file.");
                    }
                }
            }

            Console.ReadKey();


        }
    }
}
