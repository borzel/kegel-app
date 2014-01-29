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
using kegel_server.Games;
using KegelApp.Server.Database;
using KegelApp.Server.Domain;
using KegelApp.Server.Domain.Entities;
using Nancy;
using System.Collections.Generic;

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

                // TODO spieltoStart beachten
                GameBase spiel = GameFactory.CreateGame(GameEnum.HausnummerVor);
                
				if (Server.Instance.GetUsers().Any()) 
				{
					spiel.Start(Server.Instance.GetSession(), Server.Instance.GetUsers());
				}

				return Response.AsRedirect (MOUDL_BASEURL);
			};
			
			Post ["/wurf"] = _ =>
			{
                Shot wurf = new Shot();
				
				string erg = Request.Form ["punktzahl"];
				if (erg == "R") {
					wurf.Value = 0;
					wurf.NullShot = true;
				} else if (erg == "U") {
                    wurf.Value = 0;
					wurf.Fault = true;
				} else if (erg != "") {
                    wurf.Value = int.Parse(erg);
				} else
                {
                    return HttpStatusCode.InternalServerError;
                }

                GameBase spiel = GameFactory.CreateGame(Server.Instance.CurrentGame().GameId);
                spiel.SetWurf(Server.Instance.GetSession(), wurf);
                
                //Server.Instance.Save();
				
				return Response.AsRedirect (MOUDL_BASEURL);
			};
			
			Get ["/"] = _ =>
			{
				GameModel model = new GameModel ();
				
				if (Server.Instance.CurrentGame() != null && !Server.Instance.CurrentGame().Finished) {
					model.Spieler = Server.Instance.CurrentGame().CurrentUser.Name;
                    model.Erklaerung = Server.Instance.CurrentGame().Description;
                    model.Spielname = Server.Instance.CurrentGame().Name;
					model.Spiel = true;
                    model.Results = ResultCalculator.GetResult(Server.Instance.CurrentGame());
                    model.UsersToPlay = Server.Instance.CurrentGame().UsersToPlay.ToList();
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
        public List<ResultData> Results { get; set; }
        public List<User> UsersToPlay { get; set; }
		
		public GameModel ()
		{
			Spiel = false;
			Spieler = "";
			Spielname = "Es läuft gerade kein Spiel";
			Erklaerung = "nix los hier :-(";
		}
	}
}
