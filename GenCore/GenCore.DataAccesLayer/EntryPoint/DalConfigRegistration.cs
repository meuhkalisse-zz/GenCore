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
            RegisterDataAccessComponent(ref builder);
            RegisterProvider(ref builder);
        }

        private void RegisterDataAccessComponent(ref ContainerBuilder pContainer)
        {
            pContainer.Register<ISessionManager>(context => new NHibernateSessionManager(ConnectionString)).SingleInstance();
            pContainer.Register<ISession>(context => context.Resolve<ISessionManager>().OpenSession()).InstancePerDependency();
        }

        private void RegisterProvider(ref ContainerBuilder pContainer)
        {
            pContainer.Register<IColumnsProvider>(context => new ColumnsProvider(new Repository<Columns>(context.Resolve<ISession>())));
            pContainer.Register<ITablesProvider>(context => new TablesProvider(new Repository<Tables>(context.Resolve<ISession>())));
        }
    }
}
