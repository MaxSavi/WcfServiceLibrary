using KT8WcfServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Kt8_WCF_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(OrderService), new Uri("http://localhost:8733/Design_Time_Addresses/KT8WcfServiceLibrary/OrderService/mx")))
            {
                host.AddServiceEndpoint(typeof(IOrderService), new WSHttpBinding(), "");
                host.Open();
                Console.WriteLine("Sync Service is running...");
                Console.ReadLine();
            }
            
        }
    }
}
