using System;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace class_Stream
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = "File.txt";
            FileStream fileStream = File.Create(path);
            string content = "This is file content";
            byte[] contentInBytes = Encoding.UTF8.GetBytes(content);
            fileStream.Write(contentInBytes, 0, contentInBytes.Length);
            fileStream.Close();


            var permission = new FileIOPermission(FileIOPermissionAccess.Read, path);
            permission.AddPathList(FileIOPermissionAccess.Write | FileIOPermissionAccess.Read, "C:\\example\\out.txt");

            try
            {
                permission.Demand();
            }
            catch (SecurityException s)
            {
                Console.WriteLine(s.Message);
            }

            if (!permission.IsUnrestricted())
            {
                Console.WriteLine($"Cannot access {path} for writing");
            }


            int packedValue = 20;

            // The API function call sets packedValue here....
            // Convert the packed value into an array of bytes.
            byte[] valueBytes = BitConverter.GetBytes(packedValue);
            // Unpack the two values.
            short value1, value2;
            value1 = BitConverter.ToInt16(valueBytes, 0);
            value2 = BitConverter.ToInt16(valueBytes, 2);

            Console.ReadKey();

        }
    }
}
