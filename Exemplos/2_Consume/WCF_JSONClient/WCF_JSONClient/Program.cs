using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WCF_JSONClient
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(@"http://localhost:8999/NorthwindsService.svc
    Categories(1)?$select = CategoryID, CategoryName, Description");
            req.Accept = "application / json; odata = verbose";
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                Stream s = resp.GetResponseStream();
                StreamReader readStream = new StreamReader(s);
                string jsonString = readStream.ReadToEnd();
                Debug.WriteLine(jsonString);
                resp.Close();
                readStream.Close();
            }
        }
    }
}
