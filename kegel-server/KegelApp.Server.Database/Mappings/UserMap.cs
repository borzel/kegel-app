using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using KegelApp.Server.Database.Entities;

namespace KegelApp.Server.Database.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Sex);

            HasMany(x => x.Moves)
                .Inverse()
                .Cascade.All();

            HasManyToMany(x => x.GamesToPlay)
                .Cascade.All()
                .Inverse()
                .Table("GameUser");
        }
    }
}
