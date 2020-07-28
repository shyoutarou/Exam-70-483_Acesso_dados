using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Proxy
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create the proxy for your service to use its methods
            MyService.SampleServiceSoapClient proxy = new MyService.SampleServiceSoapClient();

            int addResult = proxy.Add(5, 10);
            int subtractResult = proxy.Subtract(100, 40);
            Console.WriteLine("Addition Result is: " + addResult); // Addition Result is: 15
            Console.WriteLine("Subtraction Result is: " + subtractResult); // Subtraction Result is: 60

            Console.ReadKey();
        }
    }
}
