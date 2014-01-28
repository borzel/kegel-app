using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        }

        public ISession GetSession()
        {
            if (sessionFactory == null)
                CreateKegelSessionFactory();

            return sessionFactory.OpenSession();
        }

        private ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                           .Database(SQLiteConfiguration.Standard.ShowSql().UsingFile("kegeldata.db"))
                           .Mappings(m => m.FluentMappings.AddFromAssemblyOf<KegelSessionFactory>())
                           .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                           .ExposeConfiguration(cfg => cfg.SetInterceptor(new ABCInterceptor()))
                           .BuildSessionFactory();
        }
    }

    public class ABCInterceptor : EmptyInterceptor
    {
        public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
            Debug.WriteLine("");
            Debug.WriteLine(sql.ToString());
            Debug.WriteLine("");
            return sql;
        }
    }
}
