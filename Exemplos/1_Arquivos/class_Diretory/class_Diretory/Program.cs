using System;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;

namespace class_Diretory
{
    class Program
    {
        static void Main(string[] args)
        {

            var path_Directory = @"C:\Users\x_kat\Desktop\Code_Estudos\01_Certificado_70-483\Udemy_Exam-70-483\Chapter 11\path_Directory";

            var path_DirectoryInfo = @"C:\Users\x_kat\Desktop\Code_Estudos\01_Certificado_70-483\Udemy_Exam-70-483\Chapter 11\path_DirectoryInfo";

            // Verificar a existência do diretório / pasta criado usando a classe de diretório
            if (Directory.Exists(path_Directory))
            {
                Console.WriteLine("Pasta de Diretório Existe");
                Console.WriteLine("********************");
            }
            else
            {
                // Crie um novo diretório / pasta usando a classe Directory
                DirectoryInfo directory = Directory.CreateDirectory(path_Directory);
                Console.WriteLine("O nome do directory é: " + directory.Name);
                Console.WriteLine("********************");
            }

            // Crie um novo diretório / pasta usando a classe DirectoryInfo
            DirectoryInfo directoryInfo = new DirectoryInfo(path_DirectoryInfo);

            // Verificar a existência do diretório / pasta criado usando a classe DirectoryInfo
            if (directoryInfo.Exists)
            {
                Console.WriteLine("A pasta DirectoryInfo existe");
                Console.WriteLine("********************");

                ////Using Directory Class
                //Directory.Move("Directory Folder", "../Moved Directory Folder");
                ////Using DirectoryInfo Class
                //directoryInfo.MoveTo("../Moved DirectoryInfo Folder");
            }
            else
            {
                directoryInfo.Create();
                DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();
                directorySecurity.AddAccessRule(new FileSystemAccessRule("everyone",
                FileSystemRights.ReadAndExecute, AccessControlType.Allow));

                directoryInfo.SetAccessControl(directorySecurity);

                Console.WriteLine("O nome do directoryInfo é: " + directoryInfo.Name);
                Console.WriteLine("********************");
            }


            Directory.Move(@"C:\source", @"c:\destination");
            DirectoryInfo directorymove = new DirectoryInfo(@"C:\Source");
            directoryInfo.MoveTo(@"C:\destination");


            path_Directory = @"C:\Windows";
            path_DirectoryInfo = @"C:\Inetpub";

            // Obter arquivo de um diretório específico usando a classe de diretório
            string[] fileNames = Directory.GetFiles(path_Directory);
            foreach (var name in fileNames)
            {
                Console.WriteLine("O nome é: {0}", name);
            }

            Console.WriteLine("********************");

            // Obter arquivos de um diretório específico usando a classe DirectoryInfo
            DirectoryInfo di = new DirectoryInfo(path_DirectoryInfo);
            FileInfo[] files = di.GetFiles();
            foreach (var file in files)
            {
                Console.WriteLine("O nome é: {0}", file.Name);
            }

            Console.WriteLine("********************");

            Console.ReadKey();
        }

    }
}
