using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using kegel_server.Dto;

namespace kegel_server
{
    public static class Server
    {
        public static ServerData Data { get; set; }
        public static ISpiel CurrentSpiel {get; set;}
    }
}
