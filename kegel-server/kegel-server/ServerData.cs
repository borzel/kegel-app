using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kegel_server
{
    public static class ServerData
    {
        public static List<User> ListOfUser = new List<User>();
        
        public static void Load()
        {
            // TODO Daten laden
            ListOfUser.Add(new User { Name = "Fritz Freudenberg", Nickname = "FRF" });
            ListOfUser.Add(new User { Name = "Bertram Gunert", Nickname = "GUB" });
            ListOfUser.Add(new User { Name = "Natascha Neubert", Nickname = "NEN" });
            ListOfUser.Add(new User { Name = "Ulla Düngler", Nickname = "DUU" });
        }

        public static void Save()
        {
            // TODO daten sichern
        }
    }
}
