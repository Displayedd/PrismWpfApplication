using PrismWpfApplication.Infrastructure.Interfaces;
using PrismWpfApplication.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWpfApplication.Modules.GamesModule
{
    public class GameViewModelFactory :IGameViewModelFactory
    {
        private readonly INewsService newService;
        
        public GameViewModelFactory(INewsService newsService)
        {
            if(newsService == null)
                throw new ArgumentNullException("newsService");
            this.newService = newsService;
        }

        public GameViewModel Create()
        {
            return new GameViewModel(this.newService);
        }
    }
}
