using KegelApp.Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KegelApp.Server.Domain
{
    public static class ResultCalculator
    {
        public static List<ResultData> GetResult(Game gameToAsk)
        {
            List<ResultData> results = new List<ResultData>();

            foreach (var user in gameToAsk.Moves.Select(m => m.Player))
            {
                ResultData result = new ResultData();
                result.Spieler = user.Name;
                result.Sex = user.Sex;

                int movenumber = 0;
                foreach (var spielzug in gameToAsk.Moves.Where(m => m.Player == user))
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

                results.Add(result);
            }

            // Platzvergabe
            results = results.OrderByDescending(r => r.Punktzahl).ToList();
            int platzGesamt = 1;
            int platzMaenner = 1;
            int platzFrauen = 1;

            results.ForEach(res =>
            {
                res.PlatzGesamt = platzGesamt++;

                if (res.Sex == SexEnum.Man)
                    res.PlatzMaenner = platzMaenner++;
                else
                    res.PlatzFrauen = platzFrauen++;


            });

            return results;
        }
    }
}
