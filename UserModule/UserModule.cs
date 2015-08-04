using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PrismWpfApplication.Infrastructure;
using PrismWpfApplication.Infrastructure.Interfaces;
using PrismWpfApplication.Modules.UserModule.Authenticate;
using PrismWpfApplication.Modules.UserModule.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWpfApplication.Modules.UserModule
{
    [Module(ModuleName = "UserModule")]
    public class UserModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public UserModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }

        public void Initialize()
        {
            this.container.RegisterType<IUserService, BattlenetService>(new ContainerControlledLifetimeManager());
            this.container.RegisterType<AuthenticateViewModel>();

            AuthenticateView view = container.Resolve<AuthenticateView>();
            regionManager.AddToRegion(RegionNames.ShellContentRegion, view);
            regionManager.Regions[RegionNames.ShellContentRegion].Activate(view);
        }
    }
}
