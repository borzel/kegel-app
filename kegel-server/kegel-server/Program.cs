

using System;
namespace KegelApp.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server.Start();

            bool stayOnline = true;
            while (stayOnline)
            {
                char c = Console.ReadKey().KeyChar;

                switch(c)
                {
                    case 'q':
                        stayOnline = false;
                        GameService.Save();
                        break;
                }
            }
        }
    }
}
