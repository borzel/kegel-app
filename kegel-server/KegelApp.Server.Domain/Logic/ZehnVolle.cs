using KegelApp.Ipc;
using KegelApp.Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KegelApp.Server.Domain.Logic
{
    public class ZehnVolle : GameBase
    {
        const int MAX_WUERFE = 10;
        const int MAX_SPIELZUEGE = 1;
        const string NAME = "10 Volle";
        const string ERKLAERUNG = "Jeder führt einen Spielzug mit 10 Würfen durch. Jeder Wurf geht in die Vollen, d.h. Kegel werden nach jedem Wurf wieder komplett aufgebaut. Die Punktzahl der 10 Würfe werden addiert. Die Würfe 0, 8, 1, 5, 4, 7, 1, 1, 1, 2 ergeben also die Punktzahl 30.";

        public override int GetMaxSpielzuege()
        {
            return MAX_SPIELZUEGE;
        }

        public override int GetMaxWuerfeJeSpielzug()
        {
            return MAX_WUERFE;
        }

        public override string GetErklaerung()
        {
            return ERKLAERUNG;
        }

        public override string GetName()
        {
            return NAME;
        }

        public override int GetPunkte(int punkte, int modifier)
        {
            return punkte;
        }

        public override GameEnum GetGameId()
        {
            return GameEnum.ZehnVolle;
        }
    }
}