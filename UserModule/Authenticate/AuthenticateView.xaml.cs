using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrismWpfApplication.Modules.UserModule.Authenticate
{
    /// <summary>
    /// Interaction logic for AuthenticateView.xaml
    /// </summary>
    public partial class AuthenticateView : UserControl
    {
        public static readonly DependencyProperty SecurePasswordProperty =
            DependencyProperty.RegisterAttached("SecurePassword", typeof(SecureString), typeof(AuthenticateView));

        public AuthenticateView(AuthenticateViewModel viewmodel)
        {
            this.DataContext = viewmodel;
            InitializeComponent();

            // PasswordBox binding to enable data validation
            Binding passwordBinding = new Binding(SecurePasswordProperty.Name);
            passwordBinding.Source = viewmodel;
            passwordBinding.ValidatesOnDataErrors = true;
            Passbox.SetBinding(SecurePasswordProperty, passwordBinding);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
                ((AuthenticateViewModel)this.DataContext).SecurePassword = 
                    ((PasswordBox)sender).SecurePassword;
        }
    }
}
