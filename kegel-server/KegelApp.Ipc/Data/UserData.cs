using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KegelApp.Ipc.Data
{
    public class UserData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SexEnum Sex { get; set; }

        public override string ToString()
        {
            return String.Format("Id: {0}, Name: {1}, Geschlecht: {2}", Id, Name, Sex);
        }
    }
}
