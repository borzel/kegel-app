﻿/*
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
		Guid aktuellerSpieler;
		SpielzugData aktuellerSpielzug;
		List<Guid> spielerSequenz = new List<Guid>();
		
		public string GetName()
		{
			return "Hausnummer (rückwärts)";
		}
		
		public string GetErklaerung()
		{
			return "Jeder führt einen Spielzug mit 4 Würfen durch. Jeder Wurf entspricht der Stelle einer vierstelligen Zahl, beginnend mit der letzten Stelle. Die Würfe 4, 7, 1, 1 ergeben die Zahl 1174, also die Punktzahl 1174.";
		}
		
		public void Start()
		{
			data = new SpielData();
			data.Name = GetName();
			
			// Jeder ist einmal dran
			spielerSequenz = Server.Data.ListOfUser.Select(user => user.Id).ToList();
			
			// mit dem ersten Spieler beginnen
			aktuellerSpieler = spielerSequenz.First();
			spielerSequenz.RemoveAt(0);
			
			// Spielzüge initialisieren
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
			if (anzahlWuerfe == 4)
			{				
				// Punktzahl für diesen Spielzug berechnen
				for(int i = 0; i < anzahlWuerfe; i++)
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
			return data;
		}
		
		private void NeuerSpielzug()
		{
			aktuellerSpieler = spielerSequenz.First();
			spielerSequenz.RemoveAt(0);
			
			aktuellerSpielzug = new SpielzugData();
			aktuellerSpielzug.Spiel = data.Id;
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
