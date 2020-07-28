using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebRequestResponse
{
    class Program
    {
        static void Main(string[] args)
        {
            WebRequest request = WebRequest.Create(@"http://www.microsoft.com");
            using (WebResponse resp = request.GetResponse())
            {
                using (StreamReader responseStream = new StreamReader(resp.GetResponseStream()))
                {
                    string responseText = responseStream.ReadToEnd();
                    Console.WriteLine(responseText); // Displays the HTML of the website
                }
            }
            Console.ReadKey();
        }
    }
}
