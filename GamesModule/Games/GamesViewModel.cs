using Microsoft.Practices.Prism.Mvvm;
using PrismWpfApplication.Infrastructure;
using PrismWpfApplication.Infrastructure.Interfaces;
using PrismWpfApplication.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PrismWpfApplication.Modules.GamesModule.Games
{
    public class GamesViewModel : BindableBase
    {
        private object selectedGameView = null;
        private string backgroundImage = "";
        private IEnumerable<BaseArticleViewModel> games;
        private readonly IGameService gameService;
        

        public GamesViewModel(IGameService gameService)
        {
            this.gameService = gameService;
            this.Games = new ObservableCollection<GameViewModel>(this.gameService.GetGames());
        }

        /// <summary>
        /// Get or set the selected GameViewModel. The set object should 
        /// implement the IGameViewModel interface.
        /// </summary>
        public object SelectedGameView
        {
            get { return this.selectedGameView; }
            set
            {
                object oldGameView = this.selectedGameView;
                bool changed = SetProperty(ref this.selectedGameView, value);
                if (changed)
                {
                    DisposeArticles(oldGameView);
                    Task.Run(() => GetArticlesAsync(this.selectedGameView));
                    SetBackgroundImage(this.selectedGameView);
                }

            }
        }

        public IEnumerable<BaseArticleViewModel> Games
        {
            get { return this.games; }
            set { SetProperty(ref this.games, value); }
        }

        public string BackgroundImage
        {
            get { return this.backgroundImage; }
            set { SetProperty(ref this.backgroundImage, value); }
        }

        private void SetBackgroundImage(object view)
        {
            IGameViewModel game = view as IGameViewModel;
            if (game != null)
            {
                BackgroundImage = game.BackgroundImage;
            }
        }

        private async Task GetArticlesAsync(object model)
        {
            BaseArticleViewModel viewmodel = model as BaseArticleViewModel;
            if (viewmodel != null)
            {
                await viewmodel.InitializeArticlesAsync();
            }
        }

        private void DisposeArticles(object viewmodel)
        {
            BaseArticleViewModel articleviewmodel = viewmodel as BaseArticleViewModel;
            if (articleviewmodel != null)
                articleviewmodel.DisposeArticles();
        }
    }
}
