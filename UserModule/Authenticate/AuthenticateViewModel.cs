﻿using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using PrismWpfApplication.Infrastructure;
using PrismWpfApplication.Infrastructure.Interfaces;
using PrismWpfApplication.Infrastructure.Models;
using PrismWpfApplication.Modules.UserModule.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrismWpfApplication.Modules.UserModule.Authenticate
{
    public class AuthenticateViewModel : BindableBase, IDataErrorInfo
    {
        #region fields
        private readonly IEventAggregator eventAggregator;
        private readonly IRegionManager regionManager;
        private readonly IAsyncCommand loginCommand;
        private readonly ICommand selectGameRegionCommand;
        private readonly ICommand loginAsGuestCommand;
        private readonly IUserService userService;
        private bool isLoggedIn = false;
        private bool loginFailed = false;
        private string username;
        private string notification;
        private SecureString password;
        private GameRegion selectedRegion;
        private IList<GameRegion> gameRegions;
        private string resizeMode = "CanMinimize";
        #endregion

        #region IDataError
        string IDataErrorInfo.Error { get { return Validate(null); } }
        string IDataErrorInfo.this[string columnName] { get { return Validate(columnName); } }

        private string Validate(string memberName)
        {
            string error = null;
            if (memberName == "SecurePassword")
            {
                if (SecurePassword == null || SecurePassword.Length == 0)
                {
                    error = "Enter your password";
                }
            }
            else if (memberName == "Username")
            {
                if (string.IsNullOrEmpty(Username))
                {
                    error = "Battle.net username";
                }
            }
            return error;
        }
        #endregion

        public AuthenticateViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IUserService userService)
        {
            if (eventAggregator == null)
            {
                throw new ArgumentNullException("eventAggregator");
            }
            if (regionManager == null)
            {
                throw new ArgumentNullException("regionManager");
            }
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }

            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<LoginStatusChangedEvent>().Subscribe(this.LoginStatusChanged, ThreadOption.UIThread);
            this.userService = userService;
            this.PropertyChanged += this.OnLoginPropertyChanged;
            this.PropertyChanged += this.OnCanLoginChanged;

            this.loginCommand = new AsyncCommand<UserQueryResult>(() => this.LoginAsync(), this.CanLogin);
            this.loginAsGuestCommand = new DelegateCommand(this.LoginAsGuest);
            this.selectGameRegionCommand = new DelegateCommand<object>(this.SelectGameRegion);

            this.gameRegions = this.userService.GameRegions.ToList();
            this.SelectedRegion = this.userService.HomeRegion;

            this.Notification = Resources.Disclaimer;
        }

        #region Properties
        public bool IsLoggedIn
        {
            get { return this.isLoggedIn; }
            set
            {
                SetProperty(ref this.isLoggedIn, value);
            }
        }
        public bool LoginFailed
        {
            get { return this.loginFailed; }
            set
            {
                SetProperty(ref this.loginFailed, value);
            }
        }
        public string Username
        {
            get { return this.username; }
            set { SetProperty(ref this.username, value); }
        }
        public string Notification
        {
            get { return this.notification; }
            set { SetProperty(ref this.notification, value); }
        }
        public SecureString SecurePassword
        {
            get { return this.password; }
            set { SetProperty(ref this.password, value); }
        }
        public GameRegion SelectedRegion
        {
            get { return this.selectedRegion; }
            set
            {
                bool changed = SetProperty(ref this.selectedRegion, value);
                if (changed)
                    this.userService.HomeRegion = this.selectedRegion;
            }
        }
        public IList<GameRegion> GameRegions
        {
            get { return this.gameRegions; }
            set { SetProperty(ref this.gameRegions, value); }
        }
        public string ResizeMode
        {
            get { return this.resizeMode; }
            set { SetProperty(ref this.resizeMode, value); }
        }
        #endregion

        #region Commands
        public ICommand LoginCommand { get { return this.loginCommand; } }
        public ICommand LoginAsGuestCommand { get { return this.loginAsGuestCommand; } }
        public ICommand SelectGameRegionCommand { get { return this.selectGameRegionCommand; } }
        #endregion

        private void LoginStatusChanged(IUserService userService)
        {
            IsLoggedIn = userService.IsLoggedIn;
        }

        private void OnLoginPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsLoggedIn" && !IsLoggedIn)
            {
                this.regionManager.Regions[RegionNames.ShellContentRegion].RequestNavigate("AuthenticateView", nr => { });
            }
        }

        private void Login(string user, SecureString pass)
        {
            LoginFailed = false;
            Notification = null;
            UserQueryResult result = userService.Login(user, pass);
            LoginFailed = !result.Success;
            if (LoginFailed)
                Notification = Resources.LoginFailedMessage;
            else
                Notification = null;
        }

        private async Task<UserQueryResult> LoginAsync()
        {
            LoginFailed = false;
            UserQueryResult result = await userService.LoginAsync(Username, SecurePassword);
            LoginFailed = !result.Success;
            if (LoginFailed)
                Notification = Resources.LoginFailedMessage;
            else
                Notification = null;
            return result;
        }

        private void LoginAsGuest()
        {
            this.Login("Guest", new SecureString());
        }

        private bool CanLogin(object parameter)
        {
            AsyncCommand<UserQueryResult> command = loginCommand as AsyncCommand<UserQueryResult>;
            return !string.IsNullOrEmpty(Username) &&
                SecurePassword != null &&
                SecurePassword.Length > 0 &&
                ((command != null && command.Execution == null) || (command != null && command.Execution.IsCompleted));
        }

        private void OnCanLoginChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Username" || e.PropertyName == "SecurePassword")
            {
                (this.LoginCommand as AsyncCommandBase).RaiseCanExecuteChanged();
            }
        }

        private void SelectGameRegion(object param)
        {
            GameRegion region = param as GameRegion;
            if (region != null)
                SelectedRegion = region;
        }
    }
}
