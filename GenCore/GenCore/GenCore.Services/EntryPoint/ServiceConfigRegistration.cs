using Autofac;
using GenCore.DataAccesLayer.Provider;

namespace GenCore.Services.EntryPoint
{
    public class ServiceConfigRegistration : Module
    {
        public static void AutofacRegister(ref ContainerBuilder pContainer)
        {
            RegisterServices(ref pContainer);
        }

        private static void RegisterServices(ref ContainerBuilder pContainer)
        {
            pContainer.Register<IColumnsService>(context => new ColumnsService(context.Resolve<IColumnsProvider>()));
        }
    }
}
