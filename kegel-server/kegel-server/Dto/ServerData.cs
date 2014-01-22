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
        public List<User> ListOfUser = new List<User>();
    }
}
