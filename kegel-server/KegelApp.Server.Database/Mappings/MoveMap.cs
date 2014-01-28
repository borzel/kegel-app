using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using KegelApp.Server.Database.Entities;

namespace KegelApp.Server.Database.Mappings
{
    public class MoveMap : ClassMap<Move>
    {
        public MoveMap()
        {
            Id(x => x.Id);
            Map(x => x.Score);

            References(x => x.InGame);
            References(x => x.Player);

            HasMany(x => x.Shots).Cascade.All();
        }
    }

}