using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using KegelApp.Server.Database.Entities;

namespace KegelApp.Server.Database.Mappings
{
    public class ShotMap : ClassMap<Shot>
    {
        public ShotMap()
        {
            Id(x => x.Id);
            Map(x => x.Fault);
            Map(x => x.NullShot);
            Map(x => x.Value);
            
            References(x => x.InMove);
        }
    }
}
