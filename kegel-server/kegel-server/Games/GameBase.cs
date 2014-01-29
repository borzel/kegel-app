using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using kegel_server.Dto;
using KegelApp.Server.Database;
using KegelApp.Server.Domain;
using KegelApp.Server.Domain.Entities;
using NHibernate;

namespace kegel_server.Games
{
    public abstract class GameBase
    {
        private Game spielDaten;

        public abstract string GetErklaerung();
        public abstract string GetName();
        public abstract int GetMaxSpielzuege();
        public abstract int GetMaxWuerfeJeSpielzug();
        public abstract int GetPunkte(int punkte, int modifier = 1);
        public abstract GameEnum GetGameId(); 

        public virtual void Start(ISession session, List<User> users)
        {
            spielDaten = new Game();
            spielDaten.Name = GetName();
            spielDaten.GameId = GetGameId();

            // Jeder ist einmal dran
            spielDaten.AddUsers(users);
            
            // ersten Spielzug + Spieler initialisieren
            NeuerSpielzug();

            // Speichern
            session.SaveOrUpdate(spielDaten);
            session.Flush();
        }

        public virtual void SetWurf(ISession session, Shot wurf)
        {
            spielDaten = Server.Instance.CurrentGame();

            Move currentMove = spielDaten.CurrentMove;
            currentMove.AddShot(wurf);

            // Prüfen, ob nächster Spieler dran ist
            int anzahlWuerfe = currentMove.Shots.Count();
            if (anzahlWuerfe >= GetMaxWuerfeJeSpielzug())
            {
                // Punktzahl für diesen Spielzug berechnen
                int i = 0;
                foreach (Shot s in currentMove.Shots)
                {
                    currentMove.Score += GetPunkte(s.Value, i);
                    i++;
                }

                if (spielDaten.UsersToPlay.Any())
                {
                    NeuerSpielzug();
                }
                else
                {
                    // Spiel ist zuende!
                    spielDaten.Finished = true;
                }
            }

            session.SaveOrUpdate(spielDaten);
            session.Flush();
        }

        private void NeuerSpielzug()
        {
            Move nextMove = new Move { Player = spielDaten.GetNextUser() };
            spielDaten.AddMove(nextMove);
        }
    }
}
