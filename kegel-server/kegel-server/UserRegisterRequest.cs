using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.ServiceHost;

namespace kegel_server
{
    [Route("/register/{NickName}/{Name}")]
    public class UserRegisterRequest
    {
        public string NickName { get; set; }
        public string Name { get; set; }
    }
}
