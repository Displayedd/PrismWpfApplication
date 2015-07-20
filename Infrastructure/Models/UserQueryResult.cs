using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWpfApplication.Infrastructure.Models
{
    public class UserQueryResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ResultCode Code { get; set; }

        public enum ResultCode
        {
            Success,
            LogginFailed,
            UserExists
        }
    }
}
