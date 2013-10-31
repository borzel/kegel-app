using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServiceStack.Messaging;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace kegel_server
{
    public class HelloService : Service, IGet<HelloRequest>
    {
        private static Random r = new Random();

        public object Get(HelloRequest request)
        {
            var timer = new Stopwatch();

            timer.Start();

            while (timer.ElapsedMilliseconds < 100)
                Thread.SpinWait(1000000);

            timer.Stop();

            return new HelloResponse()
                {
                    Message = "hello " + request.Name,
                    Status = "OK -> " + timer.ElapsedMilliseconds
                };
        }
    }
}
