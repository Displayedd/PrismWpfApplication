using PrismWpfApplication.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PrismWpfApplication.Infrastructure.Interfaces
{
    public interface IUserService
    {
        UserQueryResult Login(string user, SecureString pass);
        void Logout();
        UserQueryResult Register(string user, SecureString pass);
        UserInformation CurrentUser { get; set; }
        bool IsLoggedIn { get; }
        OnlineStatuses OnlineStatus { get; set; }
    }
}
