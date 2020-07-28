using System;
using System.IO;

namespace class_File
{
    class Program
    {
        static void Main(string[] args)
        {
            //perms.AddPermission(new FileIOPermission(FileIOPermissionAccess.Read, @"C:\Windows"));
            //perms.AddPermission(new FileIOPermission(FileIOPermissionAccess.Write, @"C:\Inetpub"));
            //perms.AddPermission(new RegistryPermission(RegistryPermissionAccess.Write, @"HKEY_LOCAL_MACHINE\Software"));


            //Operacoes_Arquivo.Grava_Arquivo();

            //Operacoes_Arquivo.Append_Arquivo();

            Operacoes_Arquivo.Multipla_Operacoes();

            Console.ReadKey();
        }

        static class Operacoes_Arquivo
        {
            public static void Grava_Arquivo()
            {
                // Para criar um arquivo no local atual chamado "Arquivo" usando Arquivo (Classe estática)
                File.Create("Arquivo.txt").Close();
                // Para escrever conteúdo em um arquivo chamado "Arquivo"
                File.WriteAllText("Arquivo.txt", "Este é um arquivo criado pela File Class");

                // Para ler o arquivo chamado "Arquivo"
                string fileContent = File.ReadAllText("Arquivo.txt");
                Console.WriteLine(fileContent);

                //Se exixtir arquivo, apagar e criar..
                if (File.Exists("../Copied Arquivo.txt"))
                {
                    File.Delete("../Copied Arquivo.txt");

                    // Para copiar "Arquivo" do local atual para um novo (pasta anterior)
                    File.Copy("Arquivo.txt", "../Copied Arquivo.txt");
                }
                else
                {
                    File.Copy("Arquivo.txt", "../Copied Arquivo.txt");
                }

                // Para criar um arquivo no local atual chamado "ArquivoInfo" usando a classe FileInfo
                FileInfo info = new FileInfo("ArquivoInfo.txt");
                info.Create().Close();

                //Se exixtir arquivo, apagar e criar..
                if (File.Exists("../Moved ArquivoInfo.txt"))
                {
                    File.Delete("../Moved ArquivoInfo.txt");
                    info.MoveTo("../Moved ArquivoInfo.txt");
                }
                else
                {
                    // Para mover "FileInfo" do local atual para um novo (Pasta anterior)
                    info.MoveTo("../Moved ArquivoInfo.txt");
                }
            }

            public static void Append_Arquivo()
            {
                string dummyLines = Environment.NewLine + "Esta é a primeira linha." + Environment.NewLine +
                            "Esta é a segunda linha." + Environment.NewLine +
                            "Esta é a terceira linha.";

                // Abre DummyFile.txt e acrescenta linhas. Se o arquivo não existir, crie e abra.
                File.AppendAllLines(@"Arquivo.txt", dummyLines.Split(Environment.NewLine.ToCharArray()));

                //Adicionar uma string ao conteúdp do arquivo
                File.AppendAllText(@"Arquivo.txt", Environment.NewLine + "Adicionar uma string ao arquivo");

                //Subtitui conteúdp do arquivo por um texto
                File.WriteAllText(@"Arquivo.txt", "Texto subtituido");
            }

            public static void Multipla_Operacoes()
            {
                string path = @"C:\temp\test.txt";

                //string path = @"C:\Users\x_kat\Downloads\Microsoft.Selftestengine.70-483.v2015-08-11.by.Bala.214q.vce";
                try
                {
                    var texto = File.ReadAllText(path);
                }
                catch (DirectoryNotFoundException) { }
                catch (FileNotFoundException) { }


                // Verifique se o arquivo existe ou não em um local específico
                bool isFileExists = File.Exists(@"C:\DummyFile.txt"); // retorna false

                // Copia DummyFile.txt como novo arquivo DummyFileNew.txt
                File.Copy(@"C:\DummyFile.txt", @"D:\NewDummyFile.txt");

                // Obter quando o arquivo foi acessado pela última vez
                DateTime lastAccessTime = File.GetLastAccessTime(@"C:\DummyFile.txt");

                // obtém quando o arquivo foi gravado pela última vez
                DateTime lastWriteTime = File.GetLastWriteTime(@"C:\DummyFile.txt");

                // Mover arquivo para o novo local
                File.Move(@"C:\DummyFile.txt", @"D:\DummyFile.txt");

                // Abre o arquivo e retorna o FileStream para ler bytes do arquivo
                FileStream fs = File.Open(@"D:\DummyFile.txt", FileMode.OpenOrCreate);
                
                // Abra o arquivo e retorne o StreamReader para ler a string do arquivo
                StreamReader sr = File.OpenText(@"D:\DummyFile.txt");

                // Excluir arquivo
                File.Delete(@"C:\DummyFile.txt");
            }

        }

    }
}
