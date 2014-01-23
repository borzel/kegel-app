using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using kegel_server.Dto;
using Nancy;

namespace kegel_server.Module
{
	public class ResultModule: NancyModule
	{
		public ResultModule() : base("/result")
		{
			
			Get["/"] = _ =>
			{
				List<Result> ergebnisse = new List<Result>();

				foreach (UserData user in Server.Data.ListOfUser)
				{
					ergebnisse.Add(new Result{ Spieler = user});
				}

				return View["result.html", ergebnisse];
			};
		}
	}
}
