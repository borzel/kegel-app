﻿using System;
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

        static void Main(string[] args)
        {
        	Uri serverUri = new Uri("http://localhost:80");
        	var nancyHost = new NancyHost(serverUri);
        	nancyHost.Start();

            Console.WriteLine("{0} gestartet um {1} auf {2}", AppName, DateTime.Now, serverUri);
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
