using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrismWpfApplication.Modules.GamesModule.Games;
using System.Windows;
using System.ComponentModel;
using PrismWpfApplication.Infrastructure.Interfaces;
using Moq;
using PrismWpfApplication.Infrastructure.Models;
using System.Threading;
using System.Threading.Tasks;
using PrismWpfApplication.Modules.GamesModule;

namespace GamesModule.Tests.Games
{
    [TestClass]
    public class GameViewModelFactoryFixture
    {
        [TestMethod]
        public void WhenConstructed_IntializesValues()            
        {
            //Prepare
            Mock<INewsService> mockedNewService = new Mock<INewsService>();
            mockedNewService.Setup(x => x.GetNews(It.IsAny<string[]>()));

            //Act
            GameViewModelFactory target = new GameViewModelFactory(mockedNewService.Object);

            //Verify
            Assert.IsInstanceOfType(target.Create(), typeof(GameViewModel));
        }
    }
}
