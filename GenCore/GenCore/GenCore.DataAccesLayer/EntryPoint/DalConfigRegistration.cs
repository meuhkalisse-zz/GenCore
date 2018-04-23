using Autofac;
using GenCore.DataAccesLayer.Provider;
using GenCore.Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenCore.DataAccesLayer.EntryPoint
{
    public static class DalConfigRegistration
    {
        public static void AutofacRegister(ref ContainerBuilder pContainer, string pConnectionString)
        {
            RegisterDataAccessComponent(ref pContainer, pConnectionString);
            RegisterProvider(ref pContainer);
        }

        private static void RegisterDataAccessComponent(ref ContainerBuilder pContainer, string pConnectionString)
        {
            pContainer.Register<ISessionManager>(context => new NHibernateSessionManager(pConnectionString)).SingleInstance();
            pContainer.Register<ISession>(context => context.Resolve<ISessionManager>().OpenSession()).InstancePerRequest();
        }

        private static void RegisterProvider(ref ContainerBuilder pContainer)
        {
            pContainer.Register<IColumnsProvider>(context => new ColumnsProvider(new Repository<Columns>(context.Resolve<ISession>())));
        }
    }
}
