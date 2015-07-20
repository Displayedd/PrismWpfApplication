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
        private readonly INewsService newService;

        public GameService(INewsService newService)
        {
            this.newService = newService;
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
                           select new GameViewModel(newService)
                           {
                               BackgroundImage = x.Element("BackgroundImage").Value,
                               GameId = x.Attribute("Id").Value,
                               HeaderImage = x.Element("HeaderImage").Value,
                               HeaderText = x.Element("HeaderText").Value,
                               Keywords = XElementsToStringArray(x.Element("Keywords").Descendants())
                           };

            _games = games.ToList();
            //InitializeArticles(_games);
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
            foreach(GameViewModel game in games)
            {
                game.InitializeArticles();
            }
        }
    }
}
