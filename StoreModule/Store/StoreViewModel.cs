using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWpfApplication.Modules.StoreModule.Store
{
    public class StoreViewModel : BindableBase
    {
        private string backgroundImage = "pack://application:,,,/StoreModule;component/Resources/background.jpg";

        public string BackgroundImage
        {
            get { return this.backgroundImage; }
            set { SetProperty(ref this.backgroundImage, value); }
        }
    }
}
