using PrismWpfApplication.Infrastructure.Interfaces;
using PrismWpfApplication.Infrastructure.Models;
using PrismWpfApplication.Modules.GamesModule.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrismWpfApplication.Modules.GamesModule.Services
{
    /// <summary>
    /// Factory class for GameViewModels.
    /// </summary>
    public class GameService : IGameService
    {
        private List<GameViewModel> _games;
        private readonly IGameViewModelFactory gameViewModelFactory;

        public GameService(IGameViewModelFactory gameViewModelFactory)
        {
            this.gameViewModelFactory = gameViewModelFactory;
            InitializeService();
        }

        public GameViewModel[] GetGames()
        {
            return this._games.ToArray();
        }

        private void InitializeService()
        {
            var document = XDocument.Parse(Resources.Games);
            var games = from x in document.Descendants("Game")
                        select ConstructGameViewModel(x.Element("BackgroundImage").Value,
                        x.Attribute("Id").Value,
                        x.Element("HeaderImage").Value,
                        x.Element("HeaderText").Value,
                        XElementsToStringArray(x.Element("Keywords").Descendants()));

            _games = games.ToList();
        }

        private string[] XElementsToStringArray(IEnumerable<XElement> elements)
        {
            List<string> values = new List<string>();
            foreach (XElement element in elements)
            {
                values.Add(element.Value);
            }
            return values.ToArray();
        }

        private void InitializeArticles(List<GameViewModel> games)
        {
            foreach (GameViewModel game in games)
            {
                game.InitializeArticles();
            }
        }

        private GameViewModel ConstructGameViewModel(string bgImage, string gameId, string headerImage, string headerText, string[] keywords)
        {
            GameViewModel gameViewModel = gameViewModelFactory.Create();
            gameViewModel.BackgroundImage = bgImage;
            gameViewModel.GameId = gameId;
            gameViewModel.HeaderImage = headerImage;
            gameViewModel.HeaderText = headerText;
            gameViewModel.Keywords = keywords;
            return gameViewModel;
        }
    }
}
