using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Nancy.Hosting.Self;

namespace kegel_server
{
    class Program
    {
        private static readonly string AppName = "KegelServer";
        const string url = "http://localhost:8008";

        static void Main(string[] args)
        {
            // Admin cmd -> netsh http add urlacl url=http://+:80/ user=Jeder
        	Uri serverUri = new Uri(url);
        	var nancyHost = new NancyHost(serverUri);
        	nancyHost.Start();

            Console.WriteLine("{0} gestartet um {1} auf {2}", AppName, DateTime.Now, serverUri);
            Server.Instance.Load();
            PrintHelp();

            bool stayOnline = true;
            while (stayOnline)
            {
                Console.Write("\n> ");
                char key = Console.ReadKey().KeyChar;
                Console.Write("\n");

                switch (key)
                {
                    case 's':
                        Server.Instance.Save();
                        break;
                    case 'l':
                        Server.Instance.Load();
                        break;
                    case 'q':
                        Server.Instance.Save();
                        stayOnline = false;
                        Console.WriteLine("Kegelserver ist jetzt aus!");
                        Thread.Sleep(1000);
                        break;
                    default:
                        Console.WriteLine("Unbekanntes Tastenkürzel");
                        PrintHelp();
                        break;
                }
            }
        }

        private static void PrintHelp()
        {
            Console.WriteLine();
            Console.WriteLine(" ------ Tastenkürzel --------");
            Console.WriteLine();
            Console.WriteLine("  s - Serverdaten jetzt speichern");
            Console.WriteLine("  l - Serverdaten jetzt neu laden");
            Console.WriteLine("  q - Beenden");
            Console.WriteLine();
        }
    }
}
