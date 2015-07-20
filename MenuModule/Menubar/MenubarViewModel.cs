using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using PrismWpfApplication.Infrastructure;
using PrismWpfApplication.Infrastructure.Interfaces;
using PrismWpfApplication.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PrismWpfApplication.Modules.MenuModule.Menubar
{
    public class MenubarViewModel : BindableBase
    {
        private readonly ICommand applicationCloseCommand;
        private readonly ICommand logoutCommand;
        private readonly ICommand showGamesViewCommand;
        private readonly ICommand showStoreViewCommand;
        private readonly ICommand selectOnlineStatusCommand;
        private readonly IEventAggregator eventAggregator;
        private readonly IRegionManager regionManager;
        private readonly IUserService userService;
        private string currentUsername;
        private int onlineFriendsCount = 0;
        private OnlineStatuses onlineStatus;
        public ObservableCollection<OnlineStatuses> OnlineStatusItems { get; set; }

        public MenubarViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IUserService userService)
        {
            if (regionManager == null)
                throw new ArgumentNullException("regionManager");
            if (eventAggregator == null)
                throw new ArgumentNullException("eventAggregator");
            if (userService == null)
                throw new ArgumentNullException("userService");

            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<LoginStatusChangedEvent>().Subscribe(this.LoginStatusChanged, ThreadOption.UIThread);
            this.userService = userService;
            this.OnlineStatus = userService.OnlineStatus;

            this.applicationCloseCommand = new DelegateCommand(this.ApplicationClose);
            this.showGamesViewCommand = new DelegateCommand(this.ShowGamesView);
            this.showStoreViewCommand = new DelegateCommand(this.ShowStoreView);
            this.logoutCommand = new DelegateCommand(this.Logout);
            this.selectOnlineStatusCommand = new DelegateCommand<object>(this.SelectOnlineStatus);

            this.OnlineStatusItems =
                new ObservableCollection<OnlineStatuses> { OnlineStatuses.Online, OnlineStatuses.Busy, OnlineStatuses.Away };
        }

        #region Properties
        public string Username
        {
            get { return this.currentUsername; }
            set { SetProperty(ref this.currentUsername, value); }
        }

        public OnlineStatuses OnlineStatus
        {
            get { return this.onlineStatus; }
            set { SetProperty(ref this.onlineStatus, value); }
        }

        public int OnlineFriendsCount
        {
            get { return this.onlineFriendsCount; }
            set { SetProperty(ref this.onlineFriendsCount, value); }
        }
        #endregion

        #region Commands
        public ICommand ApplicationCloseCommand { get { return this.applicationCloseCommand; } }
        public ICommand ShowGamesViewCommand { get { return this.showGamesViewCommand; } }
        public ICommand ShowStoreViewCommand { get { return this.showStoreViewCommand; } }
        public ICommand LogoutCommand { get { return this.logoutCommand; } }
        public ICommand SelectOnlineStatusCommand { get { return this.selectOnlineStatusCommand; } }
        #endregion

        private void ShowGamesView()
        {
            this.regionManager.Regions[RegionNames.MainBody].RequestNavigate("GamesView", nr => { });
        }

        private void ShowStoreView()
        {
            this.regionManager.Regions[RegionNames.MainBody].RequestNavigate("StoreView", nr => { });
        }

        private void ApplicationClose()
        {
            Application.Current.Shutdown();
        }

        private void Logout()
        {
            userService.Logout();
            this.OnlineStatus = userService.OnlineStatus;
        }

        private void SelectOnlineStatus(object status)
        {
            if (status is OnlineStatuses)
            {
                OnlineStatuses onlineStatus = (OnlineStatuses)status;
                this.userService.OnlineStatus = onlineStatus;
            }
        }

        private void LoginStatusChanged(IUserService userService)
        {
            if (this.userService.CurrentUser != null)
            {
                this.Username = this.userService.CurrentUser.Username;
                this.OnlineStatus = userService.OnlineStatus;
            }
        }
    }
}
