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

using kegel_server.Dto;

namespace kegel_server.Games
{
	/// <summary>
	/// Description of HausnummerZurueck.
	/// </summary>
	public class HausnummerZurueck : ISpiel
	{
		SpielData data;
		UserData aktuellerSpieler;
		SpielzugData aktuellerSpielzug;
		
		public string GetName()
		{
			return "Hausnummer (rückwärts)";
		}
		
		public string GetErklaerung()
		{
			return "Jeder führt einen Spielzug mit 4 Würfen durch. Jeder Wurf entspricht der Stelle einer vierstelligen Zahl, beginnend mit der letzten Stelle. Die Würfe 4, 7, 1, 1 ergeben die Zahl 1174, also die Punktzahl 1174.";
		}
		
		public void Start(List<UserData> listOfUser)
		{
			data = new SpielData();
			data.Spieler = listOfUser;
			
			// mit dem ersten Spieler beginnen
			aktuellerSpieler = data.Spieler.First();
			
			// Spielzüge initialisieren
			NeuerSpielzug();
		}
		
		public UserData GetAktuellenSpieler()
		{
			return aktuellerSpieler;
		}
		
		public bool SetWurf(WurfData wurf)
		{
			aktuellerSpielzug.Wuerfe.Add(wurf);
			
			// Prüfen, ob nächster Spieler dran ist
			if (aktuellerSpielzug.Wuerfe.Count == 4)
			{
				// Punktzahl für diesen Sielzug berechnen
				int j = aktuellerSpielzug.Wuerfe.Count-1;
				for(int i = 0; i < aktuellerSpielzug.Wuerfe.Count; i++)
				{
					aktuellerSpielzug.PunktZahl += aktuellerSpielzug.Wuerfe[i].Punktzahl * ((int)Math.Pow(10,j));
					j--;
				}
				
				int index = data.Spieler.IndexOf(aktuellerSpieler);
				if (index < (data.Spieler.Count - 1))
				{
					// nächsten aus der Liste nehmen
					aktuellerSpieler = data.Spieler[index + 1];
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
			return data;
		}
		
		private void NeuerSpielzug()
		{
			aktuellerSpielzug = new SpielzugData();
			data.Spielzuege.Add(aktuellerSpielzug);
		}
	}
}
