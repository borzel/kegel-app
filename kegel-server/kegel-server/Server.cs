using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KegelApp.Server.Database;
using KegelApp.Server.Domain.Entities;
using NHibernate;
using NHibernate.Linq;
using Nancy.Hosting.Self;
using System.Threading;
using System.Linq.Expressions;

namespace KegelApp.Server
{
	public static class Server
	{
		private static readonly string AppName = "KegelServer";
        const string url = "http://localhost:8008";

        public static void Start()
        {
            Console.WriteLine("Start Kegelserver...");

            // Admin cmd -> netsh http add urlacl url=http://+:80/ user=Jeder
            Uri serverUri = new Uri(url);
            HostConfiguration hostConfig = new HostConfiguration();
            hostConfig.UrlReservations.CreateAutomatically = true;
            hostConfig.UrlReservations.User = "Jeder";
            hostConfig.RewriteLocalhost = true;
            var nancyHost = new NancyHost(hostConfig, serverUri);
            nancyHost.Start();

            Console.WriteLine("{0} gestartet {1}", AppName, DateTime.Now);
        }
    }
}
