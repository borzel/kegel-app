﻿/*
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
		
		public string GetName()
		{
			return "Hausnummer (vorwärts)";
		}
		
		public string GetErklaerung()
		{
			return "Jeder führt einen Spielzug mit 4 Würfen durch. Jeder Wurf entspricht der Stelle einer vierstelligen Zahl, beginnend mit der 1. Stelle. Die Würfe 0, 8, 1, 5 ergeben die Zahl 0815, also die Punktzahl 815.";
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
				for(int i = 0; i < aktuellerSpielzug.Wuerfe.Count; i++)
				{
					aktuellerSpielzug.PunktZahl += aktuellerSpielzug.Wuerfe[i].Punktzahl * ((int)Math.Pow(10,i));
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
