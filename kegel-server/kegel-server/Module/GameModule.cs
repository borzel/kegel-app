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
using KegelApp.Server.Database;
using KegelApp.Server.Domain;
using KegelApp.Server.Domain.Entities;
using Nancy;
using System.Collections.Generic;
using KegelApp.Server.Domain.Logic;
using KegelApp.Server;

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
				GameService.StartGame(Request.Form ["spiel"]);
				return Response.AsRedirect (MOUDL_BASEURL);
			};
			
			Post ["/wurf"] = _ =>
			{
                GameService.AddShot(Request.Form ["punktzahl"]);
				return Response.AsRedirect (MOUDL_BASEURL);
			};
			
			Get ["/"] = _ =>
			{
				GameModel model = new GameModel ();

                if (GameService.CurrentGame() != null && !GameService.CurrentGameIsFinished())
                {
                    model.Spieler = GameService.CurrentUserNameOfCurrentGame();
                    model.Erklaerung = GameService.CurrentGame().GetErklaerung();
                    model.Spielname = GameService.CurrentGameName();
					model.Spiel = true;
                    model.Results = ResultCalculator.GetResult(GameService.CurrentGame().GameData);
                    model.UsersToPlay = GameService.CurrentGame().GameData.UsersToPlay.ToList();
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
        public List<string> Games { get; set; }
		
		public GameModel ()
		{
			Spiel = false;
			Spieler = "";
			Spielname = "Es läuft gerade kein Spiel";
			Erklaerung = "nix los hier :-(";
            Games = Enum.GetNames(typeof(GameEnum)).ToList();
		}
	}
}
