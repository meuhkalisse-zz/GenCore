using Autofac;
using GenCore.DataAccesLayer.Provider;

namespace GenCore.Services.EntryPoint
{
    public class ServiceConfigRegistration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterServices(ref builder);
        }

        private static void RegisterServices(ref ContainerBuilder pContainer)
        {
            pContainer.Register<IColumnsService>(context => new ColumnsService(context.Resolve<IColumnsProvider>()));
        }
    }
}
