using Microsoft.Practices.Prism.Mvvm;
using PrismWpfApplication.Infrastructure.Models;
using PrismWpfApplication.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System.Windows;

namespace PrismWpfApplication.Modules.GamesModule.Diablo
{
    public class DiabloViewModel : BaseArticleViewModel, IGameViewModel
    {
        private string headerImage = "pack://application:,,,/DiabloModule;component/Resources/header.png";
        private string backgroundImage = "pack://application:,,,/DiabloModule;component/Resources/background.jpg";
        private readonly ICommand installGameCommand;

        public DiabloViewModel(INewsService newService)
            : base(newService)
        {
            this.Keywords = new string[] { "Diablo", "Maintenance" };
            this.InitializeArticles(this.Keywords);

            this.installGameCommand = new DelegateCommand(this.installGame);
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
            get { return "Diablo III"; }
        }

        public ICommand InstallGameCommand { get { return this.installGameCommand; } }

        private void installGame()
        {
            Application.Current.Shutdown();
        }
    }
}
