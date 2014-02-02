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
using KegelApp.Ipc.Models;
using KegelApp.Ipc.Data;

namespace KegelApp.Server.Module
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
                    model.UsersToPlay = GameService.CurrentGame().GameData.UsersToPlay.Select(u=> new UserData { Name = u.Name, Id = u.Id, Sex = u.Sex }).ToList();
				}
				
				return View ["game", model];
			};
		}
	}
	
	
}
