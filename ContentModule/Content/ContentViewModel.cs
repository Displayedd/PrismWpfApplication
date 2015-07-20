using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using PrismWpfApplication.Infrastructure;
using PrismWpfApplication.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWpfApplication.Modules.ContentModule.Content
{
    public class ContentViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IRegionManager regionManager;
        private bool isLoggedIn;

        public ContentViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            if (eventAggregator == null)
            {
                throw new ArgumentNullException("eventAggregator");
            }
            if (regionManager == null)
            {
                throw new ArgumentNullException("regionManager");
            }

            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<LoginStatusChangedEvent>().Subscribe(this.LoginStatusChanged, ThreadOption.UIThread);
            this.PropertyChanged += this.OnLoginPropertyChanged;
        }

        public bool IsLoggedIn
        {
            get { return this.isLoggedIn; }
            set
            {
                SetProperty(ref this.isLoggedIn, value);
            }
        }

        private void LoginStatusChanged(IUserService userService)
        {
            IsLoggedIn = userService.IsLoggedIn;
        }

        private void OnLoginPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsLoggedIn" && IsLoggedIn)
            {
                this.regionManager.Regions[RegionNames.ShellContentRegion].RequestNavigate("ContentView", nr => { });
            }
        }
    }
}
