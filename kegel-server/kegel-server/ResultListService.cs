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

            foreach (User user in Server.Data.ListOfUser)
            {
                ergebnisse.Add(new Result{ Spieler = user});
            }

            return new ResultListResponse()
                {
                    Ergebnisse = ergebnisse
                };
        }
    }
}
