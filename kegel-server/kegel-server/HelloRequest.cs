using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.ServiceHost;

namespace kegel_server
{
    [Route("/hello/{Name}")]
    public class HelloRequest
    {
        public string Name { get; set; }
    }
}
