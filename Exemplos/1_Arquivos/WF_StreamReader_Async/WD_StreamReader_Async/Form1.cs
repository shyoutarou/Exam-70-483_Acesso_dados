using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WD_StreamReader_Async
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "Processando";

            await ReaderWriter_Async();

            button1.Text = "Fim";
        }

        public static async Task ReaderWriter_Async()
        {
            FileInfo fi = new FileInfo(@"DummyFile.txt");
            using (FileStream fs = fi.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    await writer.WriteAsync("Asynchronously Written Data");
                }

                using (StreamReader reader = new StreamReader(@"DummyFile.txt"))
                {
                    string result = await reader.ReadToEndAsync();
                    // Para testar
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
}
