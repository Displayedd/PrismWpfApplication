using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PrismWpfApplication.Infrastructure;
using PrismWpfApplication.Infrastructure.Interfaces;
using PrismWpfApplication.Modules.GamesModule.Games;
using PrismWpfApplication.Modules.GamesModule.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWpfApplication.Modules.GamesModule
{
    [Module(ModuleName = "GamesModule")]
    [ModuleDependency("UserModule")]
    public class GamesModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public GamesModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }

        public void Initialize()
        {
            this.container.RegisterType<INewsService, GamesNewsService>(new ContainerControlledLifetimeManager());
            this.container.RegisterType<IGameService, GameService>(new ContainerControlledLifetimeManager());
            this.container.RegisterType<IGameViewModelFactory, GameViewModelFactory>(new ContainerControlledLifetimeManager());
            this.container.RegisterType<GamesViewModel>();

            GamesView view = container.Resolve<GamesView>();
            regionManager.AddToRegion(RegionNames.MainBody, view);
            regionManager.Regions[RegionNames.MainBody].Activate(view);
        }
    }
}
