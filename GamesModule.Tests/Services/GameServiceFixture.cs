using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using System.ComponentModel;
using Moq;
using PrismWpfApplication.Modules.GamesModule.Services;
using PrismWpfApplication.Infrastructure.Interfaces;
using PrismWpfApplication.Infrastructure.Models;

namespace GamesModule.Tests.Services
{
    [TestClass]
    public class GameServiceFixture
    {
        [TestMethod]
        public void WhenConstructed_IntializesValues()
        {
            //Prepare
            Mock<INewsService> mockedNewService = new Mock<INewsService>();
            Mock<IUserService> mockedUserService = new Mock<IUserService>();
            Mock<IGameViewModelFactory> mockedGameViewModelFactory = new Mock<IGameViewModelFactory>();
            mockedGameViewModelFactory.Setup(x => x.Create()).Returns(new GameViewModel(mockedNewService.Object, mockedUserService.Object));

            //Act
            GameService service = new GameService(mockedGameViewModelFactory.Object);

            //Verify
            Assert.IsNotNull(service.GetGames()[0].BackgroundImage); 
            Assert.IsNotNull(service.GetGames()[0].GameId);
            Assert.IsNotNull(service.GetGames()[0].HeaderImage);
            Assert.IsNotNull(service.GetGames()[0].HeaderText);
            Assert.AreNotEqual(0, service.GetGames()[0].Keywords.Length);
        }
    }
}
