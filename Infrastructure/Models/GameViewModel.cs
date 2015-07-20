using Microsoft.Practices.Prism.Commands;
using PrismWpfApplication.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrismWpfApplication.Infrastructure.Models
{
    public class GameViewModel : BaseArticleViewModel, IGameViewModel
    {
        private string gameId;        
        private string headerImage;
        private string headerText;
        private string backgroundImage;
        private readonly ICommand installGameCommand;

        public GameViewModel(INewsService newService)
            : base(newService)
        {
            this.installGameCommand = new DelegateCommand(this.installGame);
        }

        public string GameId
        {
            get { return this.gameId; }
            set { SetProperty(ref this.gameId, value); }
        }

        public string HeaderImage
        {
            get { return this.headerImage; }
            set { SetProperty(ref this.headerImage, value); }
        }

        public string BackgroundImage
        {
            get { return backgroundImage; }
            set { SetProperty(ref this.backgroundImage, value); }
        }

        public string HeaderText
        {
            get { return this.headerText; }
            set { SetProperty(ref this.headerText, value); }
        }

        public ICommand InstallGameCommand { get { return this.installGameCommand; } }

        //TODO: implement method
        private void installGame()
        {
            
        }
    }
}
