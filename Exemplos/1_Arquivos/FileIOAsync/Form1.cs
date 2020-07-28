using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Diagnostics;
using System.Threading;

namespace WindowsFormsApplication2
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

            // This will get the current WORKING directory (i.e. \bin\Debug)
            string workingDirectory = Environment.CurrentDirectory;

            // This will get the current PROJECT directory
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

            string outputFileName = projectDirectory + "\\FoundFiles.txt";

            await SearchDirectory(workingDirectory, "A", outputFileName);

            button1.Text = "Finished";

            Process.Start(outputFileName);
        }

        private static async Task SearchDirectory(string searchPath, string searchString, string outputFileName)
        {
            StreamWriter streamWriter = File.CreateText(outputFileName);

            string[] fileNames = Directory.GetFiles(searchPath);

            //para Testar funcionalidade do async
            await Task.Run(() =>
            {
                for (int i = 0; i < 8; i++)
                {
                    Thread.Sleep(4000);
                    Debug.Write("Processando... ");
                }
            });

            //await FindTextInFilesAsync(fileNames, searchString, streamWriter);

            streamWriter.Close();
        }

        private static async Task FindTextInFilesAsync(string[] fileNames, string searchString, StreamWriter outputFile)
        {
            foreach (string fileName in fileNames)
            {
                if (fileName.ToLower().EndsWith(".txt"))
                {
                    StreamReader streamReader = new StreamReader(fileName);

                    string textOfFile = await streamReader.ReadToEndAsync();
                    streamReader.Close();

                    if (textOfFile.Contains(searchString))
                    {
                        await outputFile.WriteLineAsync(fileName);
                    }
                }
            }
        }
    }
}
