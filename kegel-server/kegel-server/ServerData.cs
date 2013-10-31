using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;

namespace kegel_server
{
    [Serializable]
    public class ServerData
    {
        public List<User> ListOfUser = new List<User>();
    }
}
