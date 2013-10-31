using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.WebHost.Endpoints;

namespace kegel_server
{
    //Define the Web Services AppHost
    public class AppHost : AppHostHttpListenerBase
    {
        public AppHost()
            : base("HttpListener Self-Host", typeof(UserRegisterService).Assembly) { }

        public override void Configure(Funq.Container container) { }
    }
}
