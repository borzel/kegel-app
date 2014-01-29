using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KegelApp.Server.Domain
{
    public class ResultData
    {
        public string Spieler { get; set; }
        public SexEnum Sex { get; set; }
        public string Wuerfe { get; set; }
        public int Punktzahl { get; set; }
        public int PlatzGesamt { get; set; }
        public int? PlatzMaenner { get; set; }
        public int? PlatzFrauen { get; set; }
    }
}
