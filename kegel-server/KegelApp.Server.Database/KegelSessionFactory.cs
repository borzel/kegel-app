using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using KegelApp.Server.Database.Entities;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace KegelApp.Server.Database
{
    public class KegelSessionFactory
    {
        #region Singleton
        private static KegelSessionFactory instance;

        public static KegelSessionFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new KegelSessionFactory();
                }
                return instance;
            }
        }
        #endregion

        private  ISession session;
        private  ISessionFactory sessionFactory;

        public  void CreateKegelSessionFactory()
        {
            sessionFactory = CreateSessionFactory();

            using (session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var spiel = new Game { Name = "Hausnummer vor" };

                    var herbert = new User { Name = "Herbert", Sex = SexEnum.Man };
                    var elfriede = new User { Name = "Elfriede", Sex = SexEnum.Woman };
                    var gunter = new User { Name = "Gunter", Sex = SexEnum.Man };

                    var spielzugH1 = new Move { InGame = spiel, Player = herbert, Score = 22 };
                    var spielzugH2 = new Move { InGame = spiel, Player = herbert, Score = 21 };
                    var spielzugH3 = new Move { InGame = spiel, Player = herbert, Score = 25 };

                    var spielzugE1 = new Move { InGame = spiel, Player = elfriede, Score = 4 };
                    var spielzugE2 = new Move { InGame = spiel, Player = elfriede, Score = 27 };
                    var spielzugE3 = new Move { InGame = spiel, Player = elfriede, Score = 1 };

                    var spielzugG1 = new Move { InGame = spiel, Player = gunter, Score = 2 };
                    var spielzugG2 = new Move { InGame = spiel, Player = gunter, Score = 55 };
                    var spielzugG3 = new Move { InGame = spiel, Player = gunter, Score = 1 };

                    for (int i = 0; i < 10; i++)
                    {
                        var shotH1 = new Shot { Value = 2 };
                        spielzugH1.AddShot(shotH1);
                        var shotH2 = new Shot { Value = 4 };
                        spielzugH1.AddShot(shotH2);
                        var shotH3 = new Shot { Value = 1 };
                        spielzugH1.AddShot(shotH3);
                    }

                    session.SaveOrUpdate(spiel);

                    transaction.Commit();
                }
            }
        }

        public ISession GetSession()
        {
            if (sessionFactory == null)
                CreateKegelSessionFactory();

            return sessionFactory.OpenSession();
        }

        private  ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                           .Database(SQLiteConfiguration.Standard.UsingFile("kegeldata.db"))
                           .Mappings(m => m.FluentMappings.AddFromAssemblyOf<KegelSessionFactory>())
                           .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                           .BuildSessionFactory();
        }
    }
}
