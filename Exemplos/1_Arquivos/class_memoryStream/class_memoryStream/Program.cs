using System;
using System.IO;
using System.Text;

namespace class_memoryStream
{
    class Program
    {
        static void Main(string[] args)
        {
            // Crie um objeto MemoryStream com capacidade de 100 bytes.
            MemoryStream memoryStream = new MemoryStream(10);

            byte[] javaBytes = Encoding.UTF8.GetBytes("Java ");
            byte[] csharpBytes = Encoding.UTF8.GetBytes("CSharp Comprimento");

            // Grava bytes no fluxo de memória.
            memoryStream.Write(javaBytes, 0, javaBytes.Length);
            memoryStream.Write(csharpBytes, 0, csharpBytes.Length);

            // Escreva capacidade e comprimento.
            // ==> Capacidade: 10, Comprimento: 23. Aumenta automatico para Capacidade: 256
            Console.WriteLine("Capacidade: {0}, Comprimento: {1}",
                                    memoryStream.Capacity.ToString(),
                                    memoryStream.Length.ToString());

            // Neste momento, a posição do cursor está em pé após o caractere '0'.
            // ==> 23.
            Console.WriteLine("Position:" + memoryStream.Position);

            // Mova o cursor para trás 6 bytes, comparado à posição atual.
            memoryStream.Seek(-6, SeekOrigin.Current);

            // Agora, posicione o cursor após o caractere 'i' e antes de 'm'.
            // ==> 17
            Console.WriteLine("Position:" + memoryStream.Position);

            byte[] vsBytes = Encoding.UTF8.GetBytes("vs");

            // Grava no fluxo de memória. Substituindo "im" por "vs"
            memoryStream.Write(vsBytes, 0, vsBytes.Length);

            byte[] allBytes = memoryStream.GetBuffer();

            string data = Encoding.UTF8.GetString(allBytes);

            // ==> Java vs rp
            Console.WriteLine("Conteudo lido por GetBuffer: " + data);

            // Define a posição para o início do fluxo
            memoryStream.Seek(0, SeekOrigin.Begin);
            // Ler do arquivo
            byte[] readContent = new byte[memoryStream.Length];
            int count = memoryStream.Read(readContent, 0, readContent.Length);
            for (int i = count; i < memoryStream.Length; i++)
            {
                readContent[i] = Convert.ToByte(memoryStream.ReadByte());
            }
            var resultado = Encoding.UTF8.GetString(readContent);
            Console.WriteLine("Conteudo lido por Read: " + resultado);

            Console.Read();
        }
    }
}
