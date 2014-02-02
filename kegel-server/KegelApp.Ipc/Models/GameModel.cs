using KegelApp.Ipc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KegelApp.Ipc.Models
{
    public class GameModel
    {
        public string Spieler { get; set; }
        public string Erklaerung { get; set; }
        public string Spielname { get; set; }
        public bool Spiel { get; set; }
        public List<ResultData> Results { get; set; }
        public List<UserData> UsersToPlay { get; set; }
        public List<string> Games { get; set; }

        public GameModel()
        {
            Spiel = false;
            Spieler = "";
            Spielname = "Es läuft gerade kein Spiel";
            Erklaerung = "nix los hier :-(";
            Games = Enum.GetNames(typeof(GameEnum)).ToList();
        }
    }
}
