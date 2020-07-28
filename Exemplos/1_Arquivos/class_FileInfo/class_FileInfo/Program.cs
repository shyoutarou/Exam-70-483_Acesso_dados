using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class_FileInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Cria o objeto FileInfo para o caminho especificado
            FileInfo fi = new FileInfo(@"DummyFile.txt");

            Console.WriteLine(fi.Name);

            // Abrir arquivo para leitura \ gravação
            FileStream fs = fi.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            // cria uma matriz de bytes do mesmo tamanho que o comprimento do FileStream
            byte[] fileBytes = new byte[fs.Length];

            // define contador para verificar quantos bytes ler. Diminua o contador enquanto lê cada byte
            int numBytesToRead = (int)fileBytes.Length;

            // Contador para indicar o número de bytes já lidos
            int numBytesRead = 0;

            // itera até que todos os bytes sejam lidos no FileStream
            while (numBytesToRead > 0)
            {
                int n = fs.Read(fileBytes, numBytesRead, numBytesToRead);

                if (n == 0)
                    break;

                numBytesRead += n;
                numBytesToRead -= n;
            }

            // Depois de ler todos os bytes do FileStream, você pode convertê-lo em string usando a codificação UTF8
            string filestring = Encoding.UTF8.GetString(fileBytes);

            Console.WriteLine(filestring);

            // Crie o objeto StreamWriter para gravar a string no FileSream
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.WriteLine(Environment.NewLine + "Outra linha do streamwriter");
            }

            fs.Close();

            Console.WriteLine("======Com StreamReader ===========");

            fs = fi.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            // Crie o objeto StreamReader passando o objeto FileStream no qual ele precisa operar
            StreamReader sr = new StreamReader(fs);

            // Use o método ReadToEnd para ler todo o conteúdo do arquivo
            string fileContent = sr.ReadToEnd();
            Console.WriteLine("StreamReader: " + fileContent);

            fs.Close();

            Console.WriteLine("======Com using StreamReader ===========");

            using (StreamReader reader = new StreamReader(@"DummyFile.txt"))
            {
                // Read entire text file with ReadToEnd.
                string contents = reader.ReadToEnd();
                Console.WriteLine("using StreamReader: " + contents);
            }

            fs = fi.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            ReaderWriter_Async(fs);

            Console.ReadKey();
        }

        public static async void ReaderWriter_Async(FileStream fs)
        {
            Console.WriteLine("======Com using async StreamReader ===========");

            using (StreamWriter writer = new StreamWriter(fs))
            {
                await writer.WriteAsync("Asynchronously Written Data");
            }

            using (StreamReader reader = new StreamReader(@"DummyFile.txt"))
            {
                string result = await reader.ReadToEndAsync();
                Console.WriteLine(result);
            }

            fs.Close();
        }
    }
}
