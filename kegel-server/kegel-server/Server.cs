using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KegelApp.Server.Database;
using KegelApp.Server.Database.Entities;
using NHibernate;
using kegel_server.Dto;
using kegel_server.Games;

namespace kegel_server
{
	public  class Server
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

	    private ISession session;

		private Server ()
		{
		    session = KegelSessionFactory.Instance.GetSession();
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
		}

		public void RemoveUser (User oldUser)
		{
		    session.SaveOrUpdate(GetUsers().Remove(oldUser));
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
