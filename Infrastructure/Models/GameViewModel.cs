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
        private GameRegion selectedRegion;
        private IList<GameRegion> gameRegions;
        private ICommand selectGameRegionCommand;

        public GameViewModel(INewsService newService)
            : base(newService)
        {
            this.installGameCommand = new DelegateCommand(this.installGame);
            this.selectGameRegionCommand = new DelegateCommand<object>(this.SelectGameRegion);
        }

        #region Properties
        public string BackgroundImage
        {
            get { return backgroundImage; }
            set { SetProperty(ref this.backgroundImage, value); }
        }
        public string GameId
        {
            get { return this.gameId; }
            set { SetProperty(ref this.gameId, value); }
        }
        public IList<GameRegion> GameRegions
        {
            get { return this.gameRegions; }
            set
            {
                bool changed = SetProperty(ref this.gameRegions, value);
                if (changed && this.gameRegions != null && this.gameRegions.Count > 0)
                    this.SelectedRegion = this.gameRegions.First();
            }
        }
        public string HeaderImage
        {
            get { return this.headerImage; }
            set { SetProperty(ref this.headerImage, value); }
        }
        public string HeaderText
        {
            get { return this.headerText; }
            set { SetProperty(ref this.headerText, value); }
        }
        public GameRegion SelectedRegion
        {
            get { return this.selectedRegion; }
            set { SetProperty(ref this.selectedRegion, value); }
        }
        #endregion

        public ICommand InstallGameCommand { get { return this.installGameCommand; } }
        public ICommand SelectGameRegionCommand { get { return this.selectGameRegionCommand; } }

        //TODO: implement method
        private void installGame()
        {

        }

        private void SelectGameRegion(object param)
        {
            GameRegion region = param as GameRegion;
            if (region != null)
                SelectedRegion = region;
        }
    }
}
