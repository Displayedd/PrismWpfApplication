using Microsoft.Practices.Prism.PubSubEvents;
using PrismWpfApplication.Infrastructure.Interfaces;
using PrismWpfApplication.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWpfApplication.Infrastructure
{
    public class LoginStatusChangedEvent : PubSubEvent<IUserService>
    {
    }
}
