using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace class_NetworkStream_server
{
    class Program
    {
        static void Main(string[] args)
        {
            // Precisa estar escutando na porta correta , 
            //tem que ser a mesma porta na qual o cliente vai usar

            IPAddress[] ips = Dns.GetHostAddresses("localhost");
            var adress = ips.Length == 0 ? System.Net.IPAddress.Any : ips[1];
            const int numeroPorta = 8000;
            TcpListener tcpListener = new TcpListener(adress, numeroPorta);

            tcpListener.Start();

            try
            {
                Console.WriteLine("Aguardando uma conexão...");

                // aceita a conexao do cliente e retorna uma inicializacao TcpClient 
                using (TcpClient tcpClient = tcpListener.AcceptTcpClient())
                {
                    Console.WriteLine("Conexão aceita.");

                    // obtem o stream
                    NetworkStream networkStream_read = tcpClient.GetStream();
                    NetworkStream networkStream_write = tcpClient.GetStream();

                    // qualquer comunicacao com o cliente remoto usando o TcpClient pode comecar aqui
                    string responseString = "Conectado ao servidor";
                    Byte[] sendBytes = Encoding.ASCII.GetBytes(responseString);
                    networkStream_write.Write(sendBytes, 0, sendBytes.Length);

                    int contagem = 0;
                    bool terminado = false;
                    while (!terminado)
                    {

                        // le o stream em um array de bytes
                        byte[] bytes = new byte[tcpClient.ReceiveBufferSize + 1];

                        networkStream_read.Read(bytes, 0, System.Convert.ToInt32(tcpClient.ReceiveBufferSize));

                        // Retorna os dados recebidos do cliente para o console
                        string clientdata = Encoding.ASCII.GetString(bytes);

                        clientdata = clientdata.Replace("\0", "");
                        Console.WriteLine(("Client enviou: " + clientdata));

                        sendBytes = Encoding.ASCII.GetBytes(clientdata);
                        networkStream_write.Write(sendBytes, 0, sendBytes.Length);
                        Console.WriteLine(("Mensagem enviada: " + clientdata));

                        responseString = "";
                        contagem++;

                        if (clientdata == "tchau" || clientdata == "quit"
                            || clientdata == "fim" || clientdata == "exit") terminado = true;
                    }
                }

                tcpListener.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadLine();
            }
        }
    }
}
