using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;

namespace ApplicationService.PersistentStorage.NHibernate
{
    public static class SessionManager
    {
        private static ISessionFactory _sessionFactory;
        private static volatile object _rootLock = new object();

        public static ISessionFactory GetSessionFactory()
        {
            if (_sessionFactory == null)
            {
                lock (_rootLock)
                {
                    if (_sessionFactory == null)
                    {
                        _sessionFactory = CreateSessionFactory();
                    }
                }

                return _sessionFactory;
            }
            
            return _sessionFactory;                
        }

        public static ISession OpenSession()
        {
            return GetSessionFactory().OpenSession();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2008
                        //.ShowSql()
                        .ConnectionString(@"server=.\;database=disttranslab;integrated security=true"))
                .Mappings(x => x.FluentMappings
                    .AddFromAssemblyOf<NHibernateImpl>()
                    .Conventions.Add(DefaultLazy.Never()))
                .BuildSessionFactory();
        }
    }
}