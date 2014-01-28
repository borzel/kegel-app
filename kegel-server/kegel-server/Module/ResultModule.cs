using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KegelApp.Server.Database;
using KegelApp.Server.Database.Entities;
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
				return View["result", Server.Instance.GetGames()];
			};

            Get["/{spiel_id}"] = _ =>
            {
                ResultModel model = new ResultModel();

                Game gameToAsk = Server.Instance.GetGames().FirstOrDefault(g => g.Id == _.spiel_id);
                if (gameToAsk == null)
                    return HttpStatusCode.InternalServerError;

                model.Spielname = gameToAsk.Name;

                foreach (var user in gameToAsk.Moves.Select(m => m.Player))
                {
                    ResultData result = new ResultData();
                    result.Spieler = user.Name;
                    result.Sex = user.Sex;

                    int movenumber = 0;
                    foreach (var spielzug in gameToAsk.Moves.Where(m=> m.Player == user))
                    {
                        result.Wuerfe += "(Spielzug " + movenumber + ") ";

                        foreach (Shot wurf in spielzug.Shots)
                        {
                            string erg;
                            if (wurf.Fault)
                            {
                                erg = "U";
                            }
                            else if (wurf.NullShot)
                            {
                                erg = "R";
                            }
                            else
                            {
                                erg = wurf.Value.ToString();
                            }

                            result.Wuerfe += " " + erg + " ";
                        }

                        result.Punktzahl += spielzug.Score;
                        movenumber++;
                    }

                    model.Results.Add(result);
                }

                // Platzvergabe
                model.Results = model.Results.OrderByDescending(r => r.Punktzahl).ToList();
                int platzGesamt = 1;
                int platzMaenner = 1;
                int platzFrauen = 1;

                model.Results.ForEach(res =>
                {
                    res.PlatzGesamt = platzGesamt++;

                    if (res.Sex == SexEnum.Man)
                        res.PlatzMaenner = platzMaenner++;
                    else
                        res.PlatzFrauen = platzFrauen++;


                });

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
        public SexEnum Sex { get; set; }
		public string Wuerfe {get; set;}
		public int Punktzahl {get; set;}
		public int PlatzGesamt {get; set;}
        public int? PlatzMaenner { get; set; }
        public int? PlatzFrauen { get; set; }
	}
}
