using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.UnityExtensions;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using PrismWpfApplication.Infrastructure;
using PrismWpfApplication.Infrastructure.Interfaces;
using PrismWpfApplication.Modules.UserModule.Services;

namespace PrismWpfApplication
{
    public class BootStrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
 
            Application.Current.MainWindow = (Shell)this.Shell; 
            Application.Current.MainWindow.Show(); 
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            //this.Container.RegisterType<Object, AuthenticateView>("AuthenticateView", new ContainerControlledLifetimeManager());
            //this.Container.RegisterType<Object, ContentView>("ContentView", new ContainerControlledLifetimeManager());

            //Sample code
            //this.RegisterTypeIfMissing(typeof(IModuleTracker), typeof(ModuleTracker), true);
            //this.Container.RegisterInstance<CallbackLogger>(this.callbackLogger);
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            // Directory discovery.
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        }
    }
}
