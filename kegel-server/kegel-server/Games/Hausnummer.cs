using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kegel_server.Games
{
    public abstract class Hausnummer:GameBase
    {
        const int MAX_WUERFE = 4;
        const int MAX_SPIELZUEGE = 1;

        public override int GetMaxSpielzuege()
        {
            return MAX_SPIELZUEGE;
        }

        public override int GetMaxWuerfeJeSpielzug()
        {
            return MAX_WUERFE;
        }
    }
}
