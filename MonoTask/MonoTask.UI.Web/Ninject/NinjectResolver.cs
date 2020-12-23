
using MonoTask.Core.Services;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonoTask.UI.Web.Ninject
{
    public class NinjectResolver : NinjectModule, IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectResolver()
        {
            _kernel = new StandardKernel();
            Load();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        public override void Load()
        {
            _kernel.Bind<IVehicleMakeService>().To<VehicleMakeService>().InSingletonScope();
            _kernel.Bind<IVehicleModelService>().To<VehicleModelService>().InSingletonScope();
        }
    }
}