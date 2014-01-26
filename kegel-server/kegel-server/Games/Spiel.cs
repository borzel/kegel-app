using kegel_server.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kegel_server.Games
{
    public abstract class Spiel: ISpiel
    {
        private SpielData spielDaten;
        private Guid aktuellerSpieler;
        private SpielzugData aktuellerSpielzug;
        private List<Guid> spielerSequenz;

        public abstract string GetErklaerung();
        public abstract string GetName();
        public abstract int GetMaxSpielzuege();
        public abstract int GetMaxWuerfeJeSpielzug();
        public abstract int GetPunkte(int punkte, int modifier = 1);

        public virtual void Start()
        {
            spielDaten = new SpielData();
            spielDaten.Id = Guid.NewGuid();
            spielDaten.Name = GetName();

            // Jeder ist einmal dran
            spielerSequenz = Server.Data.ListOfUser.Select(user => user.Id).ToList();

            // ersten Spielzug + Spieler initialisieren
            NeuerSpielzug();
        }

        private void NeuerSpielzug()
        {
            aktuellerSpieler = spielerSequenz.First();
            spielerSequenz.RemoveAt(0);

            aktuellerSpielzug = new SpielzugData();
            aktuellerSpielzug.Id = Guid.NewGuid();
            aktuellerSpielzug.Spiel = spielDaten.Id;
            aktuellerSpielzug.Spieler = aktuellerSpieler;
            Server.Data.Spielzuege.Add(aktuellerSpielzug);
        }

        public virtual bool SetWurf(WurfData wurf)
        {            
            wurf.Spieler = aktuellerSpieler;
            wurf.Spielzug = aktuellerSpielzug.Id;
            wurf.Wurfnummer = Server.Data.Wuerfe.Count(w => w.Spielzug == aktuellerSpielzug.Id);

            List<WurfData> wuerfeDesSpielzuges = Server.Data.Wuerfe
                .Where(w => w.Spielzug == aktuellerSpielzug.Id)
                .OrderBy(w => w.Wurfnummer).ToList();

            // Prüfen, ob nächster Spieler dran ist
            int anzahlWuerfe = wuerfeDesSpielzuges.Count();
            if (anzahlWuerfe >= GetMaxWuerfeJeSpielzug())
            {
                // Punktzahl für diesen Spielzug berechnen
                for (int i = 1; i <= anzahlWuerfe; i++)
                {
                    aktuellerSpielzug.Punktzahl += GetPunkte(wuerfeDesSpielzuges.Where(w => w.Wurfnummer == i).First().Wurfergebniss, i);
                }

                if (spielerSequenz.Any())
                {
                    NeuerSpielzug();
                }
                else
                {
                    // Spiel ist zuende!
                    return false;
                }
            }

            return true;
        }

        public SpielData GetDaten()
        {
            return spielDaten;
        }

        public Guid GetAktuellenSpieler()
        {
            return this.aktuellerSpieler;
        }        
    }
}
