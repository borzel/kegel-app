using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace kegel_server
{
    class Program
    {
        private static readonly string AppName = "KegelServer";

        static void Main(string[] args)
        {
            var listeningOn = args.Length == 0 ? "http://*:80/" : args[0];
            var appHost = new AppHost();
            appHost.Init();
            appHost.Start(listeningOn);

            Console.WriteLine("{0} gestartet um {1} auf {2}", AppName, DateTime.Now, listeningOn);
            ServerDataHelper.Load();
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
                        ServerDataHelper.Save();
                        break;
                    case 'l':
                        ServerDataHelper.Load();
                        break;
                    case 'q':
                        ServerDataHelper.Save();
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
