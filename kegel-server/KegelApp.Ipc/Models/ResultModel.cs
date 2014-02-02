using KegelApp.Ipc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KegelApp.Ipc.Models
{
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
