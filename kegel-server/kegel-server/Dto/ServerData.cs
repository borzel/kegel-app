using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace kegel_server.Dto
{
    [Serializable]
    public class ServerData
    {
        public List<UserData> ListOfUser = new List<UserData>();
        public List<SpielData> ListOfSpiele = new List<SpielData>();
    }
}
