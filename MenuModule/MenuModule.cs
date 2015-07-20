using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PrismWpfApplication.Infrastructure;
using PrismWpfApplication.Modules.MenuModule.Menubar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWpfApplication.Modules.MenuModule
{
    [Module(ModuleName = "MenuModule")]
    [ModuleDependency("UserModule")]
    public class MenuModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public MenuModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }

        public void Initialize()
        {
            this.container.RegisterType<MenubarViewModel>();
            regionManager.RegisterViewWithRegion(RegionNames.Header, 
                () => container.Resolve<MenubarView>());
        }
    }
}
