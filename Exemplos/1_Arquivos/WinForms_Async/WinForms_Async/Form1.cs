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

namespace WinForms_Async
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "Imício";

            await Tarefa();

            button1.Text = "Fim";
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
