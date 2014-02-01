using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using KegelApp.Server.Domain.Entities;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System.Data;

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

        private ISessionFactory sessionFactory;

        private KegelSessionFactory()
        {
            sessionFactory = CreateSessionFactory();
        }

        public ISession GetSession()
        {
            ISession session = sessionFactory.OpenSession();
            //session.BeginTransaction();
            return session;
        }

        private ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                           .Database(SQLiteConfiguration.Standard
#if DEBUG
                           .ShowSql().FormatSql()
#endif
                           .UsingFile("kegeldata.db"))
                           .Cache(c => c.UseSecondLevelCache())
                           .Mappings(m => m.FluentMappings.AddFromAssemblyOf<KegelSessionFactory>())
                           .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                           .BuildSessionFactory();
        }
    }
}
