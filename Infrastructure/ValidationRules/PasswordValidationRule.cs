using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PrismWpfApplication.Infrastructure.ValidationRules
{
    public class PasswordValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value,
            CultureInfo cultureInfo)
        {
            var str = value as SecureString;
            if (str == null)
                return new ValidationResult(false,
                  "Please enter Password");
            else if(str.Length == 0)
                return new ValidationResult(false,
                    "Please enter Password");
            else  
                return new ValidationResult(true, null);
        }
    }
}
