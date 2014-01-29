using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KegelApp.Server.Database;
using KegelApp.Server.Domain.Entities;
using NHibernate;
using kegel_server.Games;
using Nancy.Hosting.Self;
using System.Threading;

namespace kegel_server
{
	public class Server
	{
		#region Singleton
		private static Server instance;

		public static Server Instance {
			get {
				if (instance == null) {
					instance = new Server ();
				}
				return instance;
			}
		}
		#endregion

        private static readonly string AppName = "KegelServer";
        const string url = "http://localhost:8008";


	    private ISession session;

		private Server ()
		{
		    session = KegelSessionFactory.Instance.GetSession();
		}

        public void Start()
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

        public void Save()
        {
            session.Flush();
        }

        public ISession GetSession()
        {
            return session;
        }

		#region Users
		public List<User> GetUsers ()
		{
            List<User> users = session.CreateCriteria(typeof(User)).List<User>().ToList();
		    return users;
		}

		public void AddUser (User newUser)
		{
            session.SaveOrUpdate(newUser);
            Save();
		}

		public void RemoveUser (User oldUser)
		{
		    session.SaveOrUpdate(GetUsers().Remove(oldUser));
            Save();
		}
		#endregion

        #region Spiele
        public List<Game> GetGames()
        {
            List<Game> games = session.CreateCriteria(typeof(Game)).List<Game>().ToList();
            return games;
        } 

        public Game CurrentGame()
        {
            Game cur = session.CreateCriteria(typeof (Game)).List<Game>().OrderByDescending(x=>x.Id).FirstOrDefault();
            return cur;
        }
        #endregion
    }
}
