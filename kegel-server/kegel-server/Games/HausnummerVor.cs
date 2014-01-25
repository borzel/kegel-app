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
		SpielData spielDaten;
		Guid aktuellerSpieler;
		SpielzugData aktuellerSpielzug;
		List<Guid> spielerSequenz = new List<Guid>();
		
		public string GetName()
		{
			return "Hausnummer (vorwärts)";
		}
		
		public string GetErklaerung()
		{
			return "Jeder führt einen Spielzug mit 4 Würfen durch. Jeder Wurf entspricht der Stelle einer vierstelligen Zahl, beginnend mit der 1. Stelle. Die Würfe 0, 8, 1, 5 ergeben die Zahl 0815, also die Punktzahl 815.";
		}
		
		public void Start()
		{
			spielDaten = new SpielData();
			spielDaten.Id = Guid.NewGuid();
			spielDaten.Name = GetName();
			
			// Jeder ist einmal dran
			spielerSequenz = Server.Data.ListOfUser.Select(user => user.Id).ToList();
			
			// ersten Spielzug + Spieler initialisieren
			NeuerSpielzug();
		}
		
		public Guid GetAktuellenSpieler()
		{
			return aktuellerSpieler;
		}
		
		public bool SetWurf(WurfData wurf)
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
				for(int i = 1; i <= anzahlWuerfe; i++)
				{
					aktuellerSpielzug.Punktzahl += wuerfeDesSpielzuges.Where(w => w.Wurfnummer==i).First().Wurfergebniss * ((int)Math.Pow(10,i));
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
		
		public int GetMaxSpielzuege()
		{
			return 1;
		}
		
		public int GetMaxWuerfeJeSpielzug()
		{
			return 4;
		}
	}
}
