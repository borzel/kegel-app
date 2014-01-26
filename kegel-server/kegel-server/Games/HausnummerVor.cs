/*
 * Created by SharpDevelop.
 * User: alex
 * Date: 23.01.2014
 * Time: 22:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using kegel_server.Dto;

namespace kegel_server.Games
{
	/// <summary>
	/// Description of HausnummerVor.
	/// </summary>
    public class HausnummerVor : Hausnummer
	{
        private const string NAME = "Hausnummer (vorwärts)";
        private const string ERKLAERUNG = "Jeder führt einen Spielzug mit 4 Würfen durch. Jeder Wurf entspricht der Stelle einer vierstelligen Zahl, beginnend mit der 1. Stelle. Die Würfe 0, 8, 1, 5 ergeben die Zahl 0815, also die Punktzahl 815.";
        
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
            return punkte *((int)Math.Pow(10, base.GetMaxWuerfeJeSpielzug() - modifier));
        }        
    }
}
