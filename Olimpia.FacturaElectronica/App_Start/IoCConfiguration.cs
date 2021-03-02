using Autofac;
using Autofac.Integration.WebApi;
using Olimpia.FacturaElectronica.Business.Implementations;
using Olimpia.FacturaElectronica.Business.Interfaces;
using System.Reflection;
using System.Web.Http;

namespace Olimpia.FacturaElectronica.App_Start
{
    /// <summary>
    /// Archivo de configuración para implementar la Injeccion de dependencias
    /// </summary>
    public static class IoCConfiguration
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<FacturaBusiness>()
                   .As<IFacturaBusiness>()
                   .InstancePerRequest();

            builder.RegisterType<ValidacionesFactura>()
                   .As<IValidacionesFactura>()
                   .SingleInstance();

            ////registrar las servicios, repositorios de otras capas/proyectos de la solución
            //builder.RegisterModule<BusinessLogicModules>();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }
    }
}