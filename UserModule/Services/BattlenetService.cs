using Microsoft.Practices.Prism.PubSubEvents;
using PrismWpfApplication.Infrastructure;
using PrismWpfApplication.Infrastructure.Interfaces;
using PrismWpfApplication.Infrastructure.Models;
using PrismWpfApplication.Modules.UserModule.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrismWpfApplication.Modules.UserModule.Services
{
    public class BattlenetService : IUserService
    {
        private List<UserInformation> _users;
        private UserInformation currentUser = null;
        private ConnectionStatus connectionStatus = ConnectionStatus.LoggedOut;
        private IEventAggregator EventAggregator { get; set; }
        private OnlineStatuses onlineStatus = OnlineStatuses.Offline;

        public BattlenetService(IEventAggregator eventAggregator)
        {
            if (eventAggregator == null)
            {
                throw new ArgumentNullException("eventAggregator");
            }

            EventAggregator = eventAggregator;
            this.InitializeService();
        }

        private enum ConnectionStatus
        {
            LoggedIn,
            LoggedOut
        }

        public OnlineStatuses OnlineStatus
        {
            get { return this.onlineStatus; }
            set
            {
                if (value != this.onlineStatus)
                {
                    onlineStatus = value;
                    EventAggregator.GetEvent<LoginStatusChangedEvent>().Publish(this);
                }
            }
        }
        public UserInformation CurrentUser
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = value;
            }
        }
        public bool IsLoggedIn
        {
            get
            {
                return connectionStatus == ConnectionStatus.LoggedIn;
            }
        }

        public UserQueryResult Login(string user, SecureString pass)
        {
            // Convert password to string
            string unsecurePass = this.convertToUNSecureString(pass);

            UserInformation registerdUser = ((from users in _users
                                              where users.Username == user && users.Password == unsecurePass
                                              select users).FirstOrDefault());
            if (registerdUser != null)
            {
                currentUser = new UserInformation { Username = registerdUser.Username };
                connectionStatus = ConnectionStatus.LoggedIn;
                OnlineStatus = OnlineStatuses.Online;
                EventAggregator.GetEvent<LoginStatusChangedEvent>().Publish(this);
                return new UserQueryResult { Code = UserQueryResult.ResultCode.Success, Success = true };
            }
            return new UserQueryResult
            {
                Code = UserQueryResult.ResultCode.LogginFailed,
                Message = Resources.LoginFailedMessage,
                Success = false
            };
        }

        public async Task<UserQueryResult> LoginAsync(string userName, SecureString pass, CancellationToken token = new CancellationToken())
        {
            // Convert password to string
            string unsecurePass = this.convertToUNSecureString(pass);

            // Simulate long operation.
            await Task.Delay(TimeSpan.FromSeconds(2), token).ConfigureAwait(false);

            UserInformation registerdUser = ((from users in _users
                                              where users.Username == userName && users.Password == unsecurePass
                                              select users).FirstOrDefault());
            if (registerdUser != null)
            {
                currentUser = new UserInformation { Username = registerdUser.Username };
                connectionStatus = ConnectionStatus.LoggedIn;
                OnlineStatus = OnlineStatuses.Online;
                EventAggregator.GetEvent<LoginStatusChangedEvent>().Publish(this);
                return new UserQueryResult { Code = UserQueryResult.ResultCode.Success, Success = true };
            }
            return new UserQueryResult
            {
                Code = UserQueryResult.ResultCode.LogginFailed,
                Message = Resources.LoginFailedMessage,
                Success = false
            };
        }

        public void Logout()
        {
            CurrentUser = null;
            connectionStatus = ConnectionStatus.LoggedOut;
            OnlineStatus = OnlineStatuses.Offline;
            EventAggregator.GetEvent<LoginStatusChangedEvent>().Publish(this);
        }

        public UserQueryResult Register(string user, SecureString pass)
        {
            UserInformation registerdUser = ((from users in _users
                                              where users.Username == user
                                              select users).FirstOrDefault());
            if (registerdUser == null)
            {
                _users.Add(new UserInformation
                {
                    Username = user,
                    Password = this.convertToUNSecureString(pass)
                });
                return new UserQueryResult { Code = UserQueryResult.ResultCode.Success, Success = true };
            }
            return new UserQueryResult
            {
                Code = UserQueryResult.ResultCode.UserExists,
                Message = Resources.UserExistsMessage,
                Success = false
            };
        }

        private void InitializeService()
        {
            var document = XDocument.Parse(Resources.Users);
            var users = from x in document.Descendants("User")
                        select new UserInformation
                        {
                            Username = x.Attribute("Username").Value,
                            Password = x.Attribute("Password").Value
                        };

            _users = users.ToList();
        }

        private string convertToUNSecureString(SecureString secstrPassword)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secstrPassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
