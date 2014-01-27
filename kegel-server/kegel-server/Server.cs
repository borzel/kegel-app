using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using kegel_server.Dto;

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

		public  ServerData Data { get; set; }
		public  ISpiel CurrentSpiel { get; set; }
		private ServerDataHelper dataHelper = new ServerDataHelper();

		private Server ()
		{
		}

		#region Users
		public List<UserData> GetUsers ()
		{
			return Data.ListOfUser;
		}

		public void AddUser (UserData user)
		{
			Data.ListOfUser.Add(user);
		}

		public void RemoveUser (UserData user)
		{
			Data.ListOfUser.Remove(user);
		}
		#endregion

		public void Load()
		{
			Data = dataHelper.Load();
		}

		public void Save ()
		{
			dataHelper.Save(Data);
		}
	}
}
