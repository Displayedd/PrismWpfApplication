using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PrismWpfApplication.Infrastructure;
using PrismWpfApplication.Modules.StoreModule.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWpfApplication.Modules.StoreModule
{
    [Module(ModuleName = "StoreModule")]
    public class StoreModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public StoreModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }

        public void Initialize()
        {
            /* Add the module's views to the application's navigation structure. 
             * This is common when building composite UI applications using view discovery or view injection.
             * Subscribe to application level events or services.
             * Register shared services with the application's dependency injection container.
            */
            this.container.RegisterType<StoreViewModel>();
            regionManager.RegisterViewWithRegion(RegionNames.MainBody, 
                () => container.Resolve<StoreView>());
        }
    }
}
