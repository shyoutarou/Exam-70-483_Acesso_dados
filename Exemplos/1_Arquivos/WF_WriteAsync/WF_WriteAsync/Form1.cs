using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_WriteAsync
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "Searching...";

            await Tarefa();
            await CreateAndWriteAsyncToFile();
            await ReadAsyncHttpRequest();
            await ExecuteMultipleRequests();
            await ExecuteMultipleRequestsInParallel();

            button1.Text = "Fim";
        }

        public async Task CreateAndWriteAsyncToFile()
        {
            using (FileStream stream = new FileStream("test.dat", FileMode.Create,
            FileAccess.Write, FileShare.None, 4096, true))
            {
                byte[] data = new byte[100000];
                new Random().NextBytes(data);
                await stream.WriteAsync(data, 0, data.Length);
            }

            Debug.WriteLine("Fim CreateAndWriteAsyncToFile... ");
        }

        public async Task ReadAsyncHttpRequest()
        {
            HttpClient client = new HttpClient();
            string result = await client.GetStringAsync("http://www.microsoft.com");

            Debug.WriteLine("Fim ReadAsyncHttpRequest... ");
        }

        public async Task ExecuteMultipleRequests()
        {
            HttpClient client = new HttpClient();
            string microsoft = await client.GetStringAsync("http://www.microsoft.com");
            string msdn = await client.GetStringAsync("http://msdn.microsoft.com");
            string blogs = await client.GetStringAsync("http://blogs.msdn.com/");

            Debug.WriteLine("Fim ExecuteMultipleRequests... ");
        }

        public async Task ExecuteMultipleRequestsInParallel()
        {
            HttpClient client = new HttpClient();
            Task microsoft = client.GetStringAsync("http://www.microsoft.com");
            Task msdn = client.GetStringAsync("http://msdn.microsoft.com");
            Task blogs = client.GetStringAsync("http://blogs.msdn.com/");
            await Task.WhenAll(microsoft, msdn, blogs);

            Debug.WriteLine("Fim ExecuteMultipleRequestsInParallel... ");
        }

        private static async Task Tarefa()
        {
            string filename = @"c:\Temp\arquivo.txt";
            byte[] result;

            using (FileStream SourceStream = File.Open(filename, FileMode.Open))
            {
                result = new byte[SourceStream.Length];
                //await SourceStream.ReadAsync(result, 0, (int)SourceStream.Length);

                await Task.Run(() =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Thread.Sleep(1000);
                        Debug.Write("Processando... ");
                    }
                });
            }
        }
    }
}
