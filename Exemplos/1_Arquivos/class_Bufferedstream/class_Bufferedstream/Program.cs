using System;
using System.IO;
using System.Text;

namespace class_Bufferedstream
{
    class Program
    {
        public static void Main(string[] args)
        {
            String fileName = @"File.txt";

            FileInfo file = new FileInfo(fileName);

            // Verifique se o diretório existe.
            file.Directory.Create();

            // Crie um novo arquivo, se existir, será sobrescrito.
            // Retorna o objeto FileStream.
            using (FileStream fileStream = file.Create())
            {
                // Create BufferedStream quebra o FileStream.
                // (Especifique que o buffer é 10000 bytes).
                using (BufferedStream bs = new BufferedStream(fileStream, 10000))
                {
                    int índice = 0;
                    for (índice = 1; índice < 2000; índice++)
                    {
                        String s = "Esta é a linha" + índice + "\n";

                        byte[] bytes = Encoding.UTF8.GetBytes(s);

                        // Grava no buffer, quando o buffer estiver cheio, ele
                        // automaticamente pressiona o arquivo.
                        bs.Write(bytes, 0, bytes.Length);
                    }

                    //Set the position to the begninig of stream
                    bs.Seek(0, SeekOrigin.Begin);
                    //Read from file
                    byte[] readContent_Buff = new byte[bs.Length];
                    int count_Buff = bs.Read(readContent_Buff, 0, readContent_Buff.Length);
                    for (int i = count_Buff; i < bs.Length; i++)
                    {
                        readContent_Buff[i] = Convert.ToByte(bs.ReadByte());
                    }
                    string result = Encoding.UTF8.GetString(readContent_Buff);
                    Console.WriteLine(result);

                    // Liberando os dados restantes no buffer para o arquivo.
                    bs.Flush();
                }

            }

            Console.WriteLine("Finalizado!");
            Console.Read();
        }

    }
}
