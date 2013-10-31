using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kegel_server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerData.Load();

            var listeningOn = args.Length == 0 ? "http://*:80/" : args[0];
            var appHost = new AppHost();
            appHost.Init();
            appHost.Start(listeningOn);

            Console.WriteLine("AppHost Created at {0}, listening on {1}",
                DateTime.Now, listeningOn);

            Console.ReadKey();

            ServerData.Save();
        }
    }
}
