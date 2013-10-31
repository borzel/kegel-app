using System;
using System.IO;
using System.Threading;
using System.Xml.Serialization;

namespace kegel_server
{
    class ServerDataHelper
    {
        private static string dataFileName = "kegelserver.data";
        private static XmlSerializer serializer = new XmlSerializer(typeof(ServerData));

        public static void Load()
        {
            ServerData data = new ServerData();

            if (File.Exists(dataFileName))
            {
                FileStream file = new FileStream(dataFileName, FileMode.Open);
                data = serializer.Deserialize(file) as ServerData;
                file.Close();

                Console.WriteLine("Serverdaten aus \"{0}\" geladen", dataFileName);
            }
#if DEBUG
            // Testdaten
            else
            {
                data.ListOfUser.Add(new User { Name = "Fritz Freudenberg", Nickname = "FRF" });
                data.ListOfUser.Add(new User { Name = "Bertram Gunert", Nickname = "GUB" });
                data.ListOfUser.Add(new User { Name = "Natascha Neubert", Nickname = "NEN" });
                data.ListOfUser.Add(new User { Name = "Ulla Düngler", Nickname = "DUU" });
            }
#endif

            Server.Data = data;
        }

        public static void Save()
        {
            if (File.Exists(dataFileName))
            {
                File.Delete(dataFileName);
            }

            FileStream file = new FileStream(dataFileName, FileMode.Create);
            serializer.Serialize(file, Server.Data);
            file.Close();

            Console.WriteLine("Serverdaten nach \"{0}\" gesichert", dataFileName);
        }
    }
}
