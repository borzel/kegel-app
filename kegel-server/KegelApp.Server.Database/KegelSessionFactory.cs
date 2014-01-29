﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using KegelApp.Server.Domain.Entities;
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

        private ISession session;
        private ISessionFactory sessionFactory;

        public void CreateKegelSessionFactory()
        {
            sessionFactory = CreateSessionFactory();
            session = sessionFactory.OpenSession();
        }

        public ISession GetSession()
        {
            if (session == null)
                CreateKegelSessionFactory();

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
