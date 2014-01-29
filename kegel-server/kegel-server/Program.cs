

using System;
namespace kegel_server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server.Instance.Start();
            while(true)
            {
                Console.ReadLine();
            };
        }
    }
}
