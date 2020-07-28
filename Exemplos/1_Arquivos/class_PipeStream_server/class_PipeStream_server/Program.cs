using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace class_PipeStream_server
{
    class Program
    {
        //Variaveis globais para Conexão Anonima
        private string _pipeHandle;
        private ManualResetEventSlim _pipeHandleSet;

        static void Main(string[] args)
        {
            var p = new Program();
            p.Run(args);
        }

        public void Run(string[] args)
        {
            _pipeHandleSet = new ManualResetEventSlim(initialState: false);

            string nomePipe = args.Length == 1 ? args[0] : "PipeReaderDemo";
            PipesLeitor(nomePipe);
        }

        private void PipesLeitor(string nomePipe)
        {
            Console.WriteLine($"###  Servidor - {nomePipe}  ####");
            try
            {
                using (var pipeReader = new NamedPipeServerStream(nomePipe, PipeDirection.In))
                {
                    Console.WriteLine("Aguardando conexão de clientes....");
                    pipeReader.WaitForConnection();
                    Console.WriteLine("Cliente(writer) conectado !!");
                    const int BUFFERSIZE = 256;
                    bool terminado = false;
                    while (!terminado)
                    {
                        byte[] buffer = new byte[BUFFERSIZE];
                        int nRead = pipeReader.Read(buffer, 0, BUFFERSIZE);
                        string input = Encoding.UTF8.GetString(buffer, 0, nRead);
                        Console.WriteLine(input);
                        if (input == "tchau" || input == "quit"
                            || input == "fim" || input == "exit") terminado = true;
                    }
                }

                Console.WriteLine("Agora... pipe anônimo - reader");
                var pipeReader_Anon = new AnonymousPipeServerStream(PipeDirection.In, HandleInheritability.None);

                using (var reader = new StreamReader(pipeReader_Anon))
                {
                    //Seta valor do canal
                    _pipeHandle = pipeReader_Anon.GetClientHandleAsString();
                    Console.WriteLine($"pipe handle: {_pipeHandle}");

                    //Controle que avisa que servidor esta pronto
                    _pipeHandleSet.Set();

                    //iniciando cliente anonimo
                    Anonimo_Writer();

                    bool fim = false;
                    while (!fim)
                    {
                        string line = reader.ReadLine();
                        Console.WriteLine(line);
                        if (line == "fim") fim = true;
                    }
                    Console.WriteLine("concluindo a leitura...");
                }

                Console.WriteLine("Leitura completa.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Anonimo_Writer()
        {
            Console.WriteLine("pipe anônimo - writer");

            //Controle que aguarda o servidor estar pronto
            _pipeHandleSet.Wait();

            //Aqui é inidicado qual canal será utilizado _pipeHandle
            var pipeWriter = new AnonymousPipeClientStream(PipeDirection.Out, _pipeHandle);
            using (var writer = new StreamWriter(pipeWriter))
            {
                writer.AutoFlush = true;
                Console.WriteLine("iniciando o writer");
                for (int i = 0; i < 5; i++)
                {
                    writer.WriteLine($"Mensagem {i}");
                    Task.Delay(500).Wait();
                }
                writer.WriteLine("fim");
            }
        }
    }
}
