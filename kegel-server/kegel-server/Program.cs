

using System;
namespace kegel_server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server.Instance.Start();
            Console.ReadLine();
            Server.Instance.Save();
        }
    }
}
