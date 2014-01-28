using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KegelApp.Server.Database.Entities;

namespace kegel_server.Dto
{
    public class Result
    {
        public User Spieler { get; set;}
        public int Platz { get; set; }
    }
}
