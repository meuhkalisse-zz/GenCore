using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenCore.DataAccesLayer
{
    internal class NHibernateSessionManager : ISessionManager
    {
        private ISessionFactory _threadSessionFactory;
        private string _connectionString;

        public NHibernateSessionManager() { }
        public NHibernateSessionManager(string pConnectionString)
        {
            _connectionString = pConnectionString;
        }


        public ISession OpenSession()
        {
            return StartSession(GetSessionFactory());
        }

        public void CloseSession(ISession session, bool error = false)
        {
            if (session == null || !session.IsOpen)
                return;

            try
            {
                if (!error)
                    session.Transaction.Commit();
                else
                    session.Transaction.Rollback();
            }
            catch (Exception e)
            {
                session.Transaction.Rollback();
                throw e;
            }
            finally
            {
                session.Close();
            }
        }

        public void CloseSession(bool error = false)
        {
            var sessionFactory = GetSessionFactory();

            if (!CurrentSessionContext.HasBind(sessionFactory))
                return;

            var session = CurrentSessionContext.Unbind(sessionFactory);
            try
            {
                if (!error)
                    session.Transaction.Commit();
                else
                    session.Transaction.Rollback();
            }
            catch (Exception)
            {
                session.Transaction.Rollback();
                throw;
            }
            finally
            {
                session.Close();
            }
        }

        public ISession GetCurrentSession()
        {
            var sessionFactory = GetSessionFactory();

            if (CurrentSessionContext.HasBind(sessionFactory))
                return sessionFactory.GetCurrentSession();

            var session = sessionFactory.OpenSession();
            session.BeginTransaction();
            CurrentSessionContext.Bind(session);

            return session;
        }

        private ISession StartSession(ISessionFactory pFactory)
        {
            var session = pFactory.OpenSession();
            session.BeginTransaction();
            session.FlushMode = FlushMode.Manual;
            return session;
        }

        private ISessionFactory GetSessionFactory()
        {
            return GetThreadSessionFactory();
        }

        private ISessionFactory GetThreadSessionFactory()
        {
            return GetCurrentSessionFactory<ThreadStaticSessionContext>(ref _threadSessionFactory);
        }

        //TODO GAB Should be injected by the MVC project in Global.asax
        private ISessionFactory GetCurrentSessionFactory<T>(ref ISessionFactory sessionFactory) where T : ICurrentSessionContext
        {
            return sessionFactory ?? (sessionFactory = Fluently.Configure()
                                        .Database(MsSqlConfiguration.MsSql2012.ConnectionString(_connectionString))
                                        .Mappings(m => m.FluentMappings.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly()))
                                        .CurrentSessionContext<T>().BuildSessionFactory());
        }
    }
}
