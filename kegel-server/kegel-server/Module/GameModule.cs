/*
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
		private const string MOUDL_BASEURL = "/game";

		public GameModule () : base(MOUDL_BASEURL)
		{
			Post ["/start"] = _ =>
			{
				string spielToStart = Request.Form ["spiel"];
				Spiel spiel;
				
				switch (spielToStart) {
				case "hausnummervor":
					spiel = new HausnummerVor ();
					break;
				case "hausnummerzurueck":
					spiel = new HausnummerZurueck ();
					break;
				default:
					spiel = null;
					break;
				}

				if (Server.Instance.GetUsers ().Any ()) 
				{
					spiel.Start (Server.Instance.GetUsers ().Select (user => user.Id).ToList ());
					Server.Instance.CurrentSpiel = spiel;
					Server.Instance.Data.ListOfSpiele.Add (spiel.GetDaten ());
				

				}

				return Response.AsRedirect (MOUDL_BASEURL);
			};
			
			Post ["/wurf"] = _ =>
			{
				WurfData wurf = new WurfData ();
				wurf.Id = Guid.NewGuid ();
				Server.Instance.Data.Wuerfe.Add (wurf);
				
				string erg = Request.Form ["punktzahl"];
				if (erg == "R") {
					wurf.Wurfergebniss = 0;
					wurf.Ratte = true;
				} else if (erg == "U") {
					wurf.Wurfergebniss = 0;
					wurf.Ungueltig = true;
				} else {
					wurf.Wurfergebniss = int.Parse (erg);
				}
				
				if (!Server.Instance.CurrentSpiel.SetWurf (wurf)) {
					// Spiel ist zuende
					Server.Instance.CurrentSpiel = null;
				}
				
				
				return Response.AsRedirect (MOUDL_BASEURL);
			};
			
			Get ["/"] = _ =>
			{
				GameModel model = new GameModel ();
				
				if (Server.Instance.CurrentSpiel != null) {
					model.Spieler = Server.Instance.Data.ListOfUser.Where (u => u.Id == Server.Instance.CurrentSpiel.GetAktuellenSpieler ()).First ().Name;
					model.Erklaerung = Server.Instance.CurrentSpiel.GetErklaerung ();
					model.Spielname = Server.Instance.CurrentSpiel.GetName ();
					model.Spiel = true;
				}
				
				return View ["game", model];
			};
		}
	}
	
	public class GameModel
	{
		public string Spieler{ get; set; }

		public string Erklaerung { get; set; }

		public string Spielname { get; set; }

		public bool Spiel { get; set; }
		
		public GameModel ()
		{
			Spiel = false;
			Spieler = "";
			Spielname = "Es läuft gerade kein Spiel";
			Erklaerung = "nix los hier :-(";
		}
	}
}
