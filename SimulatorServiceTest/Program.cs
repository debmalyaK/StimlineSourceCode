using ServiceCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorServiceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(SimulatorWCFService));
            host.Open();
            Console.WriteLine("Service Started....");
            Console.ReadLine();
            host.Close();
        }
    }
}
