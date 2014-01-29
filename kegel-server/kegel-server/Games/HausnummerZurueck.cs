/*
 * Created by SharpDevelop.
 * User: alex
 * Date: 24.01.2014
 * Time: 01:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;

using KegelApp.Server.Database;
using KegelApp.Server.Domain;

namespace kegel_server.Games
{
	/// <summary>
	/// Description of HausnummerZurueck.
	/// </summary>
    public class HausnummerZurueck : Hausnummer
	{
        private const string NAME = "Hausnummer (rückwärts)";
        private const string ERKLAERUNG = "Jeder führt einen Spielzug mit 4 Würfen durch. Jeder Wurf entspricht der Stelle einer vierstelligen Zahl, beginnend mit der letzten Stelle. Die Würfe 4, 7, 1, 1 ergeben die Zahl 1174, also die Punktzahl 1174.";        	       

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
            return punkte * ((int)Math.Pow(10, modifier - 1));
        }

	    public override GameEnum GetGameId()
	    {
	        return GameEnum.HausnummerZurueck;
	    }
	}
}
