using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PrismWpfApplication.Infrastructure;
using PrismWpfApplication.Modules.ContentModule.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWpfApplication.Modules.ContentModule
{
    public class ContentModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public ContentModule(IRegionManager regionManager, IUnityContainer container)
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
            this.container.RegisterType<ContentViewModel>();

            ContentView view = container.Resolve<ContentView>();
            regionManager.AddToRegion(RegionNames.ShellContentRegion, view);

            //regionManager.RegisterViewWithRegion(RegionNames.ShellContentRegion, 
              //  () => container.Resolve<ContentView>());
        }
    }
}
