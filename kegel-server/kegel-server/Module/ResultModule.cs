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
				return View["result", Server.Data.ListOfSpiele];
			};
			
			Get["/{spiel_id}"] = _ =>
			{
				ResultModel model = new ResultModel();
				
				SpielData spiel = Server.Data.ListOfSpiele.Where(sp=>sp.Id == _.spiel_id).First();
				model.Spielname = spiel.Name;
				
				foreach(var user in Server.Data.ListOfUser)
				{
					ResultData result = new ResultData();
					result.Spieler = user.Name;
						
					foreach(var spielzug in Server.Data.Spielzuege.Where(s=>s.Spieler == user.Id && s.Spiel == spiel.Id).OrderBy(s=>s.Spielzugnummer))
					{
						result.Wuerfe += "(Spielzug " + spielzug.Spielzugnummer + ") ";

						foreach(var wurf in Server.Data.Wuerfe.Where(w=>w.Spielzug == spielzug.Id).OrderBy(w=>w.Wurfnummer))
						{
							string erg;
							if (wurf.Ungueltig)
							{
								erg = "U";
							}
							else if(wurf.Ratte)
							{
								erg = "R";
							}
							else
							{
								erg = wurf.Wurfergebniss.ToString();
							}
							
							result.Wuerfe += " " + erg + " ";
						}
						
						result.Punktzahl += spielzug.Punktzahl;
					}
					
					model.Results.Add(result);
				}
				
				// Platzvergabe
				model.Results = model.Results.OrderByDescending(r=>r.Punktzahl).ToList();
				int i = 1;
				foreach(var res in model.Results) { res.Platz = i; i++; }
				
				return View["resultdetail", model];
			};
		}
	}
	
	public class ResultModel
	{
		public ResultModel()
		{
			Results = new List<ResultData>();
		}
		
		public List<ResultData> Results {get; set;}
		public String Spielname {get; set;}
		public int Max {get; set;}
	}
	
	public class ResultData
	{
		public string Spieler {get; set;}
		public string Wuerfe {get; set;}
		public int Punktzahl {get; set;}
		public int Platz {get; set;}
	}
}
