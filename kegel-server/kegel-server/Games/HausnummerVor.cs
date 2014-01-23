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
	public class HausnummerVor : ISpiel
	{
		SpielData data;
		UserData aktuellerSpieler;
		SpielzugData aktuellerSpielzug;
		
		public HausnummerVor()
		{
		}
		
		public string GetErklaerung()
		{
			return "Jeder führt einen Spielzug mit 4 Würfen durch. Jeder Wurf entspricht der Stelle einer vierstelligen Zahl, beginnend mit der 1. Stelle. Die Würfe 0, 8, 1, 5 ergeben die Zahl 0815, also die Punktzahl 815.";
		}
	
		public void Start(List<UserData> listOfUser)
		{
			// Spieler für diese Spiel erfassen
			data = new SpielData(listOfUser);
			
			// mit dem erstrn Spieler beginnen
			aktuellerSpieler = data.Spieler.First();
			
			// Spielzüge initialisieren
			aktuellerSpielzug = new SpielzugData();
			data.Spielzuege.Add(aktuellerSpielzug);
		}
		
		public UserData GetSpieler()
		{
			return aktuellerSpieler; 
		}
		
		public bool SetWurf(WurfData wurf)
		{
			if (aktuellerSpielzug.Wuerfe.Count < 4)
			{
				aktuellerSpielzug.Wuerfe.Add(wurf);
			}
			else
			{
				// nächster Spieler ist dran 
				
				int index = data.Spieler.IndexOf(aktuellerSpieler);
				if (index < (data.Spieler.Count - 1))
				{
					// nächsten aus der Liste nehmen
					aktuellerSpieler = data.Spieler[index + 1];
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
			return data;
		}
	}
}
