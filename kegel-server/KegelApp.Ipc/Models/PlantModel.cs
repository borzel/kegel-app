using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KegelApp.Ipc.Models
{
    public class PlantModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genus { get; set; }
        public string Species { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
