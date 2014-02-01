using System;
using System.Collections.Generic;
using Nancy;

namespace KegelApp.Server.Module
{
	public class MainModule : NancyModule
	{
		public MainModule()
		{
			Get["/"] = _ =>
			{
				MainModel model = new MainModel();
				model.Info = new List<MainInfo>();
				model.Info.Add(new MainInfo{ Key = "Spieler", Value = GameService.GetUserCount().ToString()});
                model.Info.Add(new MainInfo { Key = "Aktuelles Spiel", Value = GameService.CurrentGame() == null ? "Zur Zeit läuft kein Spiel" : GameService.CurrentGameName() });
                model.Info.Add(new MainInfo { Key = "Gespielte Spiele", Value = GameService.GetGameCount().ToString() });
				
				return View["index.html", model];
			};
		}
	}
	
	public class MainModel
	{
		public List<MainInfo> Info {get; set;}
	}
	
	public class MainInfo
	{
		public string Key{get; set;}
		public string Value{get; set;}
	}
}
