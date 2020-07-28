﻿using System;
using System.Net.Sockets;
using System.Text;

namespace class_NetworkStream_client
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient tcpClient = new TcpClient();

            Console.WriteLine("Estabelecendo conexão.");

            tcpClient.Connect("127.0.0.1", 8000);

            NetworkStream networkStream_read = tcpClient.GetStream();
            NetworkStream networkStream_write = tcpClient.GetStream();

            if (networkStream_write.CanWrite & networkStream_read.CanRead)
            {
                bool completed = false;
                while (!completed)
                {
                    // Le o NetworkStream em um buffer
                    byte[] bytes = new byte[tcpClient.ReceiveBufferSize + 1];

                    string input = Console.ReadLine();
                    if (input == "tchau" || input == "quit"
                        || input == "fim" || input == "exit") completed = true;

                    networkStream_read.Read(bytes, 0, System.Convert.ToInt32(tcpClient.ReceiveBufferSize));

                    // exibe os dados recebidos do host no console
                    string returndata = Encoding.ASCII.GetString(bytes);
                    returndata = returndata.Replace("\0", "");
                    Console.WriteLine(("Host retornou : " + returndata));

                    byte[] sendBytes = Encoding.UTF8.GetBytes(input);
                    networkStream_write.Write(sendBytes, 0, sendBytes.Length);
                }
            }
            else if (!networkStream_read.CanRead)
            {
                Console.WriteLine("Não é possível enviar dados para este stream");

                tcpClient.Close();
            }
            else if (!networkStream_write.CanWrite)
            {
                Console.WriteLine("Não é possivel ler dados deste stream");

                tcpClient.Close();
            }
        }
    }
}





