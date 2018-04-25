using Autofac;
using GenCore.DataAccesLayer.Provider;
using GenCore.Services.Generator;

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
            pContainer.Register<ITablesService>(context => new TablesService(context.Resolve<ITablesProvider>()));
            pContainer.Register<ICSharpGenerator>(context => new CSharpGenerator(new TablesService(context.Resolve<ITablesProvider>()), new ColumnsService(context.Resolve<IColumnsProvider>())));
        }
    }
}
