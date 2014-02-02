using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KegelApp.Ipc.Models
{
    public class PlantsModel
    {
        public string Result { get; set; }
        public IList<PlantModel> Records { get; set; }
    }
}
