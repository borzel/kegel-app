/*
 * Created by SharpDevelop.
 * User: alex
 * Date: 23.01.2014
 * Time: 23:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
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
				string spiel = Request.Form["spiel"];
				if (spiel == "hausnummervor")
				{
					// Spiel starten
					Server.CurrentSpiel = new HausnummerVor();
					Server.Data.ListOfSpiele.Add(Server.CurrentSpiel.GetDaten());
					
					// Spieler eintragen
					Server.CurrentSpiel.Start(Server.Data.ListOfUser);
				}
				
				return Response.AsRedirect("/spiel");
			};
			
			Get["/"] = _ =>
			{
				return "";
			};
		}
	}
}
