﻿/*
 * Created by SharpDevelop.
 * User: alex
 * Date: 23.01.2014
 * Time: 23:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using kegel_server.Dto;
using kegel_server.Games;
using Nancy;

namespace kegel_server.Module
{
	/// <summary>
	/// Description of GameModule.
	/// </summary>
	public class GameModule : NancyModule
	{
		public GameModule() : base("/spiel")
		{
			Post["/start"] = _ =>
			{
				string spielToStart = Request.Form["spiel"];
				ISpiel spiel;
				
				switch(spielToStart)
				{
					case "hausnummervor":
						spiel = new HausnummerVor();
						break;
					case "hausnummerzurueck":
						spiel = new HausnummerZurueck();
						break;
					default:
						spiel = null;
						break;
				}
				
				spiel.Start();
				Server.CurrentSpiel = spiel;
				Server.Data.ListOfSpiele.Add(spiel.GetDaten());
				
				return Response.AsRedirect("/spiel");
			};
			
			Post["/wurf"] = _ =>
			{
				WurfData wurf = new WurfData();
				wurf.Id = Guid.NewGuid();
				Server.Data.Wuerfe.Add(wurf);
				
				string erg = Request.Form["punktzahl"];
				if (erg == "R")
				{
					wurf.Wurfergebniss = 0;
					wurf.Ratte = true;
				}
				else if(erg== "U")
				{
					wurf.Wurfergebniss = 0;
					wurf.Ungueltig = true;
				}
				else
				{
					wurf.Wurfergebniss = int.Parse(erg);
				}
				
				if (!Server.CurrentSpiel.SetWurf(wurf))
				{
					// Spiel ist zuende
					Server.CurrentSpiel = null;
				}
				
				
				return Response.AsRedirect("/spiel");
			};
			
			Get["/"] = _ =>
			{
				GameModel model = new GameModel();
				
				if (Server.CurrentSpiel != null)
				{
					model.Spieler = Server.Data.ListOfUser.Where(u => u.Id == Server.CurrentSpiel.GetAktuellenSpieler()).First().Name;
					model.Erklaerung =  Server.CurrentSpiel.GetErklaerung();
					model.Spielname = Server.CurrentSpiel.GetName();
					model.Spiel = true;
				}
				
				return View["game", model];
			};
		}
	}
	
	public class GameModel
	{
		public string Spieler{get; set;}
		public string Erklaerung {get; set;}
		public string Spielname {get; set;}
		public bool Spiel {get; set;}
		
		public GameModel()
		{
			Spiel = false;
			Spieler = "";
			Spielname = "Es läuft gerade kein Spiel";
			Erklaerung = "nix los hier :-(";
		}
	}
}
