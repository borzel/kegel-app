using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace kegel_server
{
    public class ResultListService : Service, IGet<ResultListRequest>
    {
        public object Get(ResultListRequest request)
        {
            List<Result> ergebnisse = new List<Result>();

            
            ergebnisse.Add(new Result { Platz = 1, Spieler = new User { Name = "Fritz Freudenberg", Nickname = "FRF" } });
            ergebnisse.Add(new Result { Platz = 2, Spieler = new User { Name = "Bertram Gunert", Nickname = "GUB" } });
            ergebnisse.Add(new Result { Platz = 3, Spieler = new User { Name = "Natascha Neubert", Nickname = "NEN" } });
            ergebnisse.Add(new Result { Platz = 4, Spieler = new User { Name = "Ulla Düngler", Nickname = "DUU" } });


            return new ResultListResponse()
                {
                    Ergebnisse = ergebnisse
                };
        }
    }
}
