using KegelApp.Ipc;
using KegelApp.Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KegelApp.Server.Domain.Logic
{
    public abstract class GameBase
    {
        public abstract string GetErklaerung();
        public abstract string GetName();
        public abstract int GetMaxSpielzuege();
        public abstract int GetMaxWuerfeJeSpielzug();
        public abstract int GetPunkte(int punkte, int modifier = 1);
        public abstract GameEnum GetGameId();

        public Game GameData { get; set; }

        public static GameBase CreateGame(GameEnum game)
        {
            GameBase spiel = null;

            switch (game)
            {
                case GameEnum.HausnummerVor:
                    spiel = new HausnummerVor();
                    break;
                case GameEnum.HausnummerZurueck:
                    spiel = new HausnummerZurueck();
                    break;
                case GameEnum.ZehnVolle:
                    spiel = new ZehnVolle();
                    break;
            }

            return spiel;
        }

        public GameBase()
        {
            GameData = new Game();

            GameData.Description = GetErklaerung();
            GameData.GameId = GetGameId();
            GameData.Name = GetName();
            GameData.Moves = new List<Move>();
            GameData.UsersToPlay = new List<User>();
        }

        public virtual void Start(List<User> users)
        {
            foreach (User user in users)
            {
                GameData.UsersToPlay.Add(user);
            }

            // ersten Spielzug + Spieler initialisieren
            NewMove();
        }

        public virtual void AddShot(Shot wurf)
        {
            GameData.CurrentMove.AddShot(wurf);

            // Prüfen, ob nächster Spieler dran ist
            if (GameData.CurrentMove.Shots.Count() >= GetMaxWuerfeJeSpielzug())
            {
                // Punktzahl für diesen Spielzug berechnen
                int i = 0;
                foreach (Shot s in GameData.CurrentMove.Shots)
                {
                    GameData.CurrentMove.Score += GetPunkte(s.Value, i++);
                }

                if (GameData.UsersToPlay.Any())
                {
                    NewMove();
                }
                else
                {
                    // Spiel ist zuende!
                    GameData.Finished = true;
                }
            }
        }

        private void NewMove()
        {
            if (!GameData.UsersToPlay.Any())
                throw new IndexOutOfRangeException("Es ist kein neuer Spielzug möglich -> Spieler sind alle.");

            User nextUser = GameData.UsersToPlay.First();
            GameData.UsersToPlay.Remove(nextUser);
            GameData.Moves.Add(new Move { Player = nextUser });
        }
    
    }
}
