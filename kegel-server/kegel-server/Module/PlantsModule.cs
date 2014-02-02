using KegelApp.Ipc.Models;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kegel_server.Module
{
    public class PlantsModule : NancyModule
    {
        public PlantsModule() : base("/test")
        {
            //Get["/"] = parameters => View["Plants"];
            Get["/"] = _ =>
            {
                return View["Plants.html"];
            };
            Get["/plants/{Id}"] = parameters => View[GetPlantModels().SingleOrDefault(x => x.Id == parameters.Id)];
            Get["/plants"] = parameters =>
            {
                int start = Convert.ToInt32(Request.Query.iDisplayStart.ToString());
                int length = Convert.ToInt32(Request.Query.iDisplayLength.ToString());
                var totalRecords = GetPlantModels().Count;
                var secho = Request.Query.sEcho;
                var sorting = Request.Query.sSortDir_0;
                if (sorting == "asc")
                {
                    return Response.AsJson(new { aaData = GetPlantModels().OrderBy(x => x.Id).Skip(start).Take(length), sEcho = secho, iTotalRecords = totalRecords, iTotalDisplayRecords = totalRecords });
                }
                else
                {
                    return Response.AsJson(new { aaData = GetPlantModels().OrderByDescending(x => x.Id).Skip(start).Take(length), sEcho = secho.ToString(), iTotalRecords = totalRecords, iTotalDisplayRecords = totalRecords });
                }
            };
        }

        private IList<PlantModel> GetPlantModels()
        {
            var plantModels = new List<PlantModel>();
            for (var i = 1; i <= 25; i++)
            {
                var j = i.ToString("000");
                plantModels.Add(new PlantModel()
                {
                    Id = i,
                    Name = "name" + j,
                    Genus = "genus" + j,
                    Species = "Species" + j,
                    DateAdded = DateTime.Now
                });
            }
            return plantModels;
        }
    }
}
