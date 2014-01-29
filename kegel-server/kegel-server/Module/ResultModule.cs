using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KegelApp.Server.Database;
using KegelApp.Server.Domain;
using KegelApp.Server.Domain.Entities;
using Nancy;

namespace kegel_server.Module
{
    public class ResultModule : NancyModule
    {
        public ResultModule()
            : base("/result")
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
                model.Results = ResultCalculator.GetResult(gameToAsk);
                
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

        public List<ResultData> Results { get; set; }
        public String Spielname { get; set; }
        public int Max { get; set; }
    }


}
