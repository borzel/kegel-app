using System;
using System.IO;
using System.Threading;
using System.Xml.Serialization;

using kegel_server.Dto;

namespace kegel_server
{
	class ServerDataHelper
	{
		private string dataFileName = "kegelserver.data";
		private XmlSerializer serializer = new XmlSerializer(typeof(ServerData));

		public ServerData Load()
		{
			ServerData data = new ServerData();

			if (File.Exists(dataFileName))
			{
				FileStream file = new FileStream(dataFileName, FileMode.Open);
				try
				{
					data = serializer.Deserialize(file) as ServerData;
					Console.WriteLine("Serverdaten aus \"{0}\" geladen", dataFileName);
				}
				catch
				{
					data = new ServerData();
					Console.WriteLine("Ladefehler! Serverdaten neu und leer erzeugt.");
				}
				file.Close();
			}

			return data;
		}

		public void Save(ServerData data)
		{
			if (File.Exists(dataFileName))
			{
				File.Delete(dataFileName);
			}

			FileStream file = new FileStream(dataFileName, FileMode.Create);
			serializer.Serialize(file, data);
			file.Close();

			Console.WriteLine("Serverdaten nach \"{0}\" gesichert", dataFileName);
		}
	}
}
