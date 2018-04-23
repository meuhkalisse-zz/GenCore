using Autofac;
using GenCore.DataAccesLayer.Provider;
using GenCore.Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenCore.DataAccesLayer.EntryPoint
{
    public class DalConfigRegistration : Module
    {
        public string ConnectionString { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterDataAccessComponent(ref builder, "");
            RegisterProvider(ref builder);
        }

        private static void RegisterDataAccessComponent(ref ContainerBuilder pContainer, string pConnectionString)
        {
            pContainer.Register<ISessionManager>(context => new NHibernateSessionManager(pConnectionString)).SingleInstance();
            pContainer.Register<ISession>(context => context.Resolve<ISessionManager>().OpenSession()).InstancePerLifetimeScope();
        }

        private static void RegisterProvider(ref ContainerBuilder pContainer)
        {
            pContainer.Register<IColumnsProvider>(context => new ColumnsProvider(new Repository<Columns>(context.Resolve<ISession>())));
        }
    }
}
