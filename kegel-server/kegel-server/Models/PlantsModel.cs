using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kegel_server.Models
{
    public class PlantsModel
    {
        public string Result { get; set; }
        public IList<PlantModel> Records { get; set; }
    }
}
