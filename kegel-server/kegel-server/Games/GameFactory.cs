using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KegelApp.Server.Database;
using KegelApp.Server.Domain;
using KegelApp.Server.Domain.Entities;

namespace kegel_server.Games
{
    public class GameFactory
    {
        public static GameBase CreateGame(GameEnum gameToStart)
        {
            GameBase spiel;

            switch (gameToStart)
            {
                case GameEnum.HausnummerVor:
                    spiel = new HausnummerVor();
                    break;
                case GameEnum.HausnummerZurueck:
                    spiel = new HausnummerZurueck();
                    break;
                default:
                    spiel = null;
                    break;
            }
            return spiel;
        }
    }
}
