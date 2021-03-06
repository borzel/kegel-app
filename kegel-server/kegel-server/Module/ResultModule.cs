﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KegelApp.Server.Database;
using KegelApp.Server.Domain;
using KegelApp.Server.Domain.Entities;
using Nancy;
using KegelApp.Server;
using KegelApp.Server.Domain.Logic;
using KegelApp.Ipc.Models;

namespace KegelApp.Server.Module
{
    public class ResultModule : NancyModule
    {
        public ResultModule()
            : base("/result")
        {

            Get["/"] = _ =>
            {
                return View["result", GameService.GetAllGames()];
            };

            Get["/{spiel_id}"] = _ =>
            {
                ResultModel model = new ResultModel();


                GameBase gameToAsk = GameService.FindGameById(Int32.Parse(_.spiel_id));
                if (gameToAsk == null)
                    return HttpStatusCode.InternalServerError;

                model.Spielname = gameToAsk.GetName();
                model.Results = GameService.GetGameResult(gameToAsk);
                
                return View["resultdetail", model];
            };
        }
    }

    


}
