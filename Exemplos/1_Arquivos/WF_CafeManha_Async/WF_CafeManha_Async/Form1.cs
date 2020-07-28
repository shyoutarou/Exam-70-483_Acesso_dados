using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_CafeManha_Async
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

            MainSync(lblEncherXicara, lblFritarOvos, lblFritarBacon, lblTorrarPao, lblPassarManteiga, lblPassarGeleia);
            await MainAsync(lblEncherXicara_Async, lblFritarOvos_Async, lblFritarBacon_Async, lblTorrarPao_Async, lblPassarManteiga_Async, lblPassarGeleia_Async);

            button1.Text = "Fim";
        }

        static void MainSync(Label lblXic, Label lblOvo, Label lblBac, Label lblPao, Label lblMant, Label lblGel)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            EncherXicara(stopwatch, lblXic);

            lblXic.Text += Environment.NewLine + ">>>>> XICARA CHEIA!" + stopwatch.ElapsedMilliseconds;

            FritarOvos(stopwatch, lblOvo);

            lblOvo.Text += Environment.NewLine + ">>>>> OVOS PRONTOS!" + stopwatch.ElapsedMilliseconds;

            FritarBacon(stopwatch, lblBac);

            lblBac.Text += Environment.NewLine + ">>>>> BACON PRONTO!" + stopwatch.ElapsedMilliseconds;

            TorrarPao(stopwatch, lblPao);

            PassarManteiga("tarefas[0]TORRADA PRONTA!" + stopwatch.ElapsedMilliseconds, lblMant);
            PassarGeleia("tarefas[1]TORRADA PRONTA!" + stopwatch.ElapsedMilliseconds, lblGel);

            lblPao.Text += Environment.NewLine + ">>>>> TORRADA PRONTA!" + stopwatch.ElapsedMilliseconds;

            stopwatch.Stop();

            lblGel.Text += "Cafe da manha PRONTO:" + stopwatch.ElapsedMilliseconds;
        }

        private static void EncherXicara(Stopwatch stopwatch, Label lblXic)
        {
            //Thread.Sleep(TimeSpan.FromSeconds(3));
            stopwatch.Stop();

            lblXic.Text = "EncherXicara:" + stopwatch.ElapsedMilliseconds + Environment.NewLine;
            stopwatch.Start();

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(500);
                lblXic.Text += ", Encher Xicara...";
            }
        }

        private static void FritarOvos(Stopwatch stopwatch, Label lblOvo)
        {
            stopwatch.Stop();

            lblOvo.Text = "FritarOvos:" + stopwatch.ElapsedMilliseconds + Environment.NewLine;
            stopwatch.Start();

            for (int i = 0; i < 8; i++)
            {
                Thread.Sleep(500);
                lblOvo.Text += ", Mexe no Ovo... ";
            }
        }

        private static void FritarBacon(Stopwatch stopwatch, Label lblBac)
        {
            stopwatch.Stop();

            lblBac.Text = "FritarBacon:" + stopwatch.ElapsedMilliseconds + Environment.NewLine;
            stopwatch.Start();

            for (int i = 0; i < 8; i++)
            {
                Thread.Sleep(200);
                lblBac.Text += ", Fritar Bacon... ";
            }
        }

        private static void TorrarPao(Stopwatch stopwatch, Label lblPao)
        {
            stopwatch.Stop();

            lblPao.Text = "TorrarPao:" + stopwatch.ElapsedMilliseconds + Environment.NewLine;
            stopwatch.Start();

            for (int i = 0; i < 8; i++)
            {
                Thread.Sleep(700);
                lblPao.Text += ", Torrar Pao... ";
            }
        }

        private static void PassarManteiga(string msg, Label lblMant)
        {
            for (int i = 0; i < 8; i++)
            {
                Thread.Sleep(1000);
                lblMant.Text += ", Passar Manteiga... ";
            }
        }

        private static void PassarGeleia(string msg, Label lblGel)
        {
            for (int i = 0; i < 8; i++)
            {
                Thread.Sleep(500);
                lblGel.Text += ", Passar Geleia... ";
            }
        }

        static async Task MainAsync(Label lblXic, Label lblOvo, Label lblBac, Label lblPao, Label lblMant, Label lblGel)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var Cafe = EncherXicaraAsync(stopwatch, lblXic);
            var Ovos = FritarOvosAsync(stopwatch, lblOvo);
            var Bacons = FritarBaconAsync(stopwatch, lblBac);
            var Torrada = TorrarPaoAsync(stopwatch, lblPao);

            var allTasks = new List<Task> { Cafe, Ovos, Bacons, Torrada };

            using (var tokenSource = new CancellationTokenSource())
            {
                while (allTasks.Any())
                {
                    Task finished = await Task.WhenAny(allTasks);
                    if (finished == Cafe)
                    {
                        lblXic.Text += Environment.NewLine + ">>>>> XICARA CHEIA!" + stopwatch.ElapsedMilliseconds;
                    }
                    else if (finished == Ovos)
                    {
                        lblOvo.Text += Environment.NewLine + ">>>>> OVOS PRONTOS!" + stopwatch.ElapsedMilliseconds;
                    }
                    else if (finished == Bacons)
                    {
                        lblBac.Text += Environment.NewLine + ">>>>> BACON PRONTO!" + stopwatch.ElapsedMilliseconds;
                    }
                    else if (finished == Torrada)
                    {
                        try
                        {
                            CancellationToken token = tokenSource.Token;

                            Task[] tarefas = new Task[2];

                            tarefas[0] = PassarManteigaAsync("tarefas[0]TORRADA PRONTA!" + stopwatch.ElapsedMilliseconds, token, lblMant);
                            tarefas[1] = PassarGeleiaAsync("tarefas[1]TORRADA PRONTA!" + stopwatch.ElapsedMilliseconds, token, lblGel);
                        }
                        catch (TaskCanceledException)
                        {
                            Console.WriteLine("Task was cancelled");
                        }

                        lblPao.Text += Environment.NewLine + ">>>>> TORRADA PRONTA!" + stopwatch.ElapsedMilliseconds;
                    }

                    allTasks.Remove(finished);

                }

                if (allTasks.Count == 0)
                {
                    // Cancel the task
                    tokenSource.Cancel();
                }
            }

            stopwatch.Stop();

            lblGel.Text += "Cafe da manha PRONTO:" + stopwatch.ElapsedMilliseconds;
        }

        private static async Task<Stopwatch> EncherXicaraAsync(Stopwatch stopwatch, Label lblXic)
        {
            //await Task.Delay(TimeSpan.FromSeconds(3));
            stopwatch.Stop();
            lblXic.Text = "EncherXicara:" + stopwatch.ElapsedMilliseconds + Environment.NewLine;
            stopwatch.Start();

            for (int i = 0; i < 10; i++)
            {
                await Task.Run(() =>
                            {
                                Thread.Sleep(500);
                            });

                lblXic.Text += ", Encher Xicara...";
            }

            return stopwatch;
        }

        private static async Task<Stopwatch> FritarOvosAsync(Stopwatch stopwatch, Label lblOvo)
        {
            stopwatch.Stop();
            lblOvo.Text = "FritarOvos:" + stopwatch.ElapsedMilliseconds + Environment.NewLine;
            stopwatch.Start();

            for (int i = 0; i < 8; i++)
            {
                await Task.Run(() =>
                            {
                                Thread.Sleep(500);
                            });
                lblOvo.Text += ", Mexe no Ovo... ";
            }

            return stopwatch;
        }

        private static async Task<Stopwatch> FritarBaconAsync(Stopwatch stopwatch, Label lblBac)
        {
            stopwatch.Stop();
            lblBac.Text = "FritarBacon:" + stopwatch.ElapsedMilliseconds + Environment.NewLine;
            stopwatch.Start();

            for (int i = 0; i < 8; i++)
            {
                await Task.Run(() =>
                            {
                                Thread.Sleep(200);
                            });
                lblBac.Text += ", Fritar Bacon... ";
            }

            return stopwatch;
        }

        private static async Task<Stopwatch> TorrarPaoAsync(Stopwatch stopwatch, Label lblPao)
        {
            stopwatch.Stop();
            lblPao.Text = "TorrarPao:" + stopwatch.ElapsedMilliseconds + Environment.NewLine;
            stopwatch.Start();

            for (int i = 0; i < 8; i++)
            {
                await Task.Run(() =>
                {
                    Thread.Sleep(700);
                });

                lblPao.Text += ", Torrar Pao... ";
            }

            return stopwatch;
        }

        private static async Task PassarManteigaAsync(string msg, CancellationToken token, Label lblMant)
        {
            for (int i = 0; i < 8; i++)
            {
                if (token.IsCancellationRequested)
                {
                    lblMant.Text += $"In iteration {i + 1}, cancellation has been requested...";
                    break;
                }
                await Task.Run(() =>
                        {
                            Thread.Sleep(1000);
                        });
                lblMant.Text += ", Passar Manteiga... ";
            }
        }

        private static async Task PassarGeleiaAsync(string msg, CancellationToken token, Label lblGel)
        {
            for (int i = 0; i < 8; i++)
            {
                if (token.IsCancellationRequested)
                {
                    lblGel.Text += $"In iteration {i + 1}, cancellation has been requested...";
                    break;
                }

                await Task.Run(() =>
                        {
                            Thread.Sleep(500);
                        });
                lblGel.Text += ", Passar Geleia... ";
            }
        }

    }
}
