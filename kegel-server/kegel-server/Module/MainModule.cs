﻿using System;
using System.Collections.Generic;
using Nancy;

namespace kegel_server.Module
{
	public class MainModule : NancyModule
	{
		public MainModule()
		{
			Get["/"] = _ =>
			{
				MainModel model = new MainModel();
				model.Info = new List<MainInfo>();
				model.Info.Add(new MainInfo{ Key = "Spieler", Value = Server.Instance.GetUsers().Count.ToString()});
				model.Info.Add(new MainInfo{ Key = "Aktuelles Spiel", Value = Server.Instance.CurrentGame()==null ? "Zur Zeit läuft kein Spiel": Server.Instance.CurrentGame().Name});
				model.Info.Add(new MainInfo{ Key = "Gespielte Spiele", Value = Server.Instance.GetGames().Count.ToString()});
				
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
