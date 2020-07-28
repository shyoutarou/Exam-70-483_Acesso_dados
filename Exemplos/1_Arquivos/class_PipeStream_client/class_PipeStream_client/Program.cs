using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;

namespace class_PipeStream_client
{
    class Program
    {
        static void Main(string[] args)
        {
            string nomePipe = args.Length >= 1 ? args[0] : "PipeReaderDemo";
            string nomeServidor = args.Length >= 2 ? args[1] : "localhost";
            PipesWriter(nomeServidor, nomePipe);
        }

        private static void PipesWriter(string nomeServidor, string nomePipe)
        {
            Console.WriteLine($"### Servidor - {nomePipe} ####");
            try
            {
                // Caso o nomeServidor estiver incorreto: Erro "O caminho da rede não foi encontrado.\r\n"
                // Caso o nomePipe estiver incorreto: Não há conexão e fica aguandando....
                using (var pipeWriter = new NamedPipeClientStream(nomeServidor, nomePipe, PipeDirection.Out))
                {
                    Console.WriteLine("Tentando conexão com servidor " + nomePipe + "....");
                    pipeWriter.Connect();
                    Console.WriteLine("Servidor(reader) conectado");
                    Console.WriteLine("Iniciando o writer, comece a digitar...");

                    bool completed = false;
                    while (!completed)
                    {
                        string input = Console.ReadLine();
                        if (input == "tchau" || input == "quit"
                            || input == "fim" || input == "exit") completed = true;

                        byte[] buffer = Encoding.UTF8.GetBytes(input);
                        pipeWriter.Write(buffer, 0, buffer.Length);
                    }
                }

                Console.WriteLine("Escrita terminada...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
