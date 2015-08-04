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
        private readonly IUserService userService;
        
        public GameViewModelFactory(INewsService newsService, IUserService userService)
        {
            if(newsService == null)
                throw new ArgumentNullException("newsService");
            if(userService == null)
                throw new ArgumentNullException("userService");
            this.newService = newsService;
            this.userService = userService;
        }

        public GameViewModel Create()
        {
            return new GameViewModel(this.newService, this.userService);
        }
    }
}
