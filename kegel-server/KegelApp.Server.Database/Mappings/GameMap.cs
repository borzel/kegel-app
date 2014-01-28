﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using KegelApp.Server.Database.Entities;

namespace KegelApp.Server.Database.Mappings
{
    public class GameMap : ClassMap<Game>
    {
        public GameMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);

            HasMany(x => x.Moves)
                .Inverse();
        }
    }
}
