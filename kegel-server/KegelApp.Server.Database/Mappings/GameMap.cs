using System;
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
            Map(x => x.Finished);
            Map(x => x.Description);
            Map(x => x.GameId);

            HasMany(x => x.Moves).Cascade.All();

            HasManyToMany(x => x.UsersToPlay)
                .Cascade.All()
                .Table("GameUser");
        }
    }
}
