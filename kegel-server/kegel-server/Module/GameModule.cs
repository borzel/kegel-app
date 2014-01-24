/*
 * Created by SharpDevelop.
 * User: alex
 * Date: 23.01.2014
 * Time: 23:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
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
				
				spiel.Start(Server.Data.ListOfUser);
				Server.CurrentSpiel = spiel;
				Server.Data.ListOfSpiele.Add(spiel.GetDaten());
				
				return Response.AsRedirect("/spiel");
			};
			
			Post["/wurf"] = _ =>
			{
				WurfData wurf = new WurfData();
				wurf.Punktzahl = int.Parse(Request.Form["punktzahl"]);
				
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
					model.Spieler = Server.CurrentSpiel.GetAktuellenSpieler().Name;
					model.Erklaerung =  Server.CurrentSpiel.GetErklaerung();
					model.Spielname = Server.CurrentSpiel.GetName();
					model.KeinSpiel = false;
					model.KannWurf = true;
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
		public bool KeinSpiel {get; set;}
		public bool KannWurf {get; set;}
		
		public GameModel()
		{
			KeinSpiel = true;
			KannWurf = false;
			Spieler = "";
			Spielname = "Es läuft gerade kein Spiel";
			Erklaerung = "nix los hier :-(";
		}
	}
}
