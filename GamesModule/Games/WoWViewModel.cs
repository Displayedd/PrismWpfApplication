using Microsoft.Practices.Prism.Mvvm;
using PrismWpfApplication.Infrastructure.Models;
using PrismWpfApplication.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PrismWpfApplication.Modules.GamesModule.WoW
{
    public class WoWViewModel : BaseArticleViewModel, IGameViewModel
    {
        private string headerImage = "pack://application:,,,/WowModule;component/Resources/header.png";
        private string backgroundImage = "pack://application:,,,/WowModule;component/Resources/background.jpg";

        public WoWViewModel(INewsService newService)
            : base(newService)
        {
            this.Keywords = new string[] { "World of Warcraft", "Maintenance" };
            this.InitializeArticles(this.Keywords);
        }

        public string HeaderImage
        {
            get { return headerImage; }
            set { SetProperty(ref this.headerImage, value); }
        }

        public string BackgroundImage
        {
            get { return backgroundImage; }
            set { SetProperty(ref this.backgroundImage, value); }
        }

        public string HeaderText
        {
            get { return "World of Warcraft"; }
        }
    }
}
