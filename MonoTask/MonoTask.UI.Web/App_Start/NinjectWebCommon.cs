[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MonoTask.UI.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MonoTask.UI.Web.App_Start.NinjectWebCommon), "Stop")]

namespace MonoTask.UI.Web.App_Start
{
    using System;
    using System.Web;
    using global::Ninject;
    using global::Ninject.Web.Common;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using MonoTask.Core.Services;
    using MonoTask.Infrastructure.DAL;
    using MonoTask.Infrastructure.DAL.Entities;
    using Ninject;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //TODO: Check and change if so - this is only reason to have EF in UI 
            kernel.Bind<IVehiclesDbContext>().To<VehiclesDbContext>();
            kernel.Bind<IVehicleService>().To<VehicleService>();
        }        
    }
}
