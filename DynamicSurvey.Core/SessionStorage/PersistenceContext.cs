using System;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace DynamicSurvey.Core.SessionStorage
{
    public class PersistenceContext
    {
        private static string _connectionString;
        private static ISessionFactory _sessionFactory;
        private static Configuration _nhibernateConfiguration;

        public static void SetConnectionString(string connectionString)
        {
            if (_connectionString == null)
            {
                _connectionString = connectionString;
            }
            else
            {
                throw new ArgumentException("Connection string already set.");
            }
        }

        public static Configuration GetNHibernateConfiguration()
        {
            if (_nhibernateConfiguration == null)
            {
                Init();
            }

            return _nhibernateConfiguration;
        }

        public static ISession GetCurrentSession()
        {
            var sessionStorageContainer = SessionStorageContainerFactory.GetStorageContainer();

            var currentSession = sessionStorageContainer.GetCurrentSession();

            if (currentSession != null)
            {
                return currentSession;
            }

            currentSession = GetNewSession();
            sessionStorageContainer.Store(currentSession);

            return currentSession;
        }

        public static void DisposeCurrentSession()
        {
            var sessionStorageContainer = SessionStorageContainerFactory.GetStorageContainer();

            var currentSession = sessionStorageContainer.GetCurrentSession();

            if (currentSession == null)
            {
                return;
            }

            currentSession.Dispose();
            sessionStorageContainer.Store(null);
        }

        private static void Init()
        {
            var databaseConfiguration = MySQLConfiguration.Standard.ConnectionString(_connectionString);

            _nhibernateConfiguration = Fluently.Configure()
                .Database(databaseConfiguration)
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildConfiguration();

            // tell NHibernate to quote columns with reserved names like "Order"
            // note it affects all queries, not only a schema updater
            SchemaMetadataUpdater.QuoteTableAndColumns(_nhibernateConfiguration);

            _sessionFactory = _nhibernateConfiguration.BuildSessionFactory();
        }

        private static ISessionFactory GetSessionFactory()
        {
            if (_sessionFactory == null)
            {
                Init();
            }

            return _sessionFactory;
        }

        private static ISession GetNewSession()
        {
            return GetSessionFactory().OpenSession();
        }
    }
}
