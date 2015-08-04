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

namespace GamesModule.Tests.Games
{
    [TestClass]
    public class GamesViewModelFixture
    {
        [TestMethod]
        public void WhenConstructed_IntializesValues()            
        {
            //Prepare
            Mock<INewsService> mockedNewService = new Mock<INewsService>();
            mockedNewService.Setup(x => x.GetNews(It.IsAny<string[]>()));
            Mock<IUserService> mockedUserService = new Mock<IUserService>();

            GameViewModel game = new GameViewModel(mockedNewService.Object, mockedUserService.Object);
            game.BackgroundImage = "testimage";

            Mock<IGameService> mockedGameService = new Mock<IGameService>();
            mockedGameService.Setup(x => x.GetGames()).Returns(new GameViewModel[] { game }).Verifiable();

            GamesViewModel viewmodel = new GamesViewModel(mockedGameService.Object);
            
            //Act
            viewmodel.SelectedGameView = game;

            //Verify
            Assert.AreEqual(game, viewmodel.SelectedGameView);
            Assert.AreEqual("testimage", viewmodel.BackgroundImage);
            mockedGameService.VerifyAll();
        }

        [TestMethod]
        public void WhenSelectedViewChanged_PropertyIsUpdated()            
        {
            //Prepare
            Mock<INewsService> mockedNewService = new Mock<INewsService>();
            mockedNewService.Setup(x => x.GetNews(It.IsAny<string[]>()));
            Mock<IUserService> mockedUserService = new Mock<IUserService>();

            GameViewModel game = new GameViewModel(mockedNewService.Object, mockedUserService.Object);
            game.BackgroundImage = "testimage";

            Mock<IGameService> mockedGameService = new Mock<IGameService>();
            mockedGameService.Setup(x => x.GetGames()).Returns(new GameViewModel[] { game }).Verifiable();

            GamesViewModel viewmodel = new GamesViewModel(mockedGameService.Object);

            bool backgroundImageChangedRaised = false;
            viewmodel.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "BackgroundImage")
                {
                    backgroundImageChangedRaised = true;
                }
            };

            bool selectedGameViewChangedRaised = false;
            viewmodel.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "SelectedGameView")
                {
                    selectedGameViewChangedRaised = true;
                }
            };

            //Act
            viewmodel.SelectedGameView = game;

            //Verify
            Assert.AreSame(game, viewmodel.SelectedGameView);
            Assert.IsTrue(backgroundImageChangedRaised);
            Assert.IsTrue(selectedGameViewChangedRaised);
        }

        [TestMethod]
        public void WhenSelectedViewChanged_ArticlesInitilized()
        {
            //Prepare 
            Mock<INewsService> mockedNewsService = new Mock<INewsService>();
            Article[] articles = new Article[] { 
                new Article { ArticleType = ArticleTypes.Major, Keywords = new string[] { "Diablo" } },
                new Article { ArticleType = ArticleTypes.Notification, Keywords = new string[] { "Maintenance" } }
            };

            mockedNewsService.Setup(x => x.GetNewsAsync(It.Is<string[]>(keywords => keywords.Length > 0), It.IsAny<CancellationToken>())).Returns(Task.FromResult(articles));

            Mock<IUserService> mockedUserService = new Mock<IUserService>();

            GameViewModel game = new GameViewModel(mockedNewsService.Object, mockedUserService.Object);
            game.BackgroundImage = "testimage";
            game.Keywords = new string[] { "Diablo", "Maintenance" };

            Mock<IGameService> mockedGameService = new Mock<IGameService>();
            mockedGameService.Setup(x => x.GetGames()).Returns(new GameViewModel[] { game }).Verifiable();

            GamesViewModel target = new GamesViewModel(mockedGameService.Object);

            //Act
            target.SelectedGameView = game;
            Thread.Sleep(3000);

            //Verify
            Assert.IsNotNull(game.MajorArticles);
            Assert.IsNotNull(game.MinorArticles);
        }
    }
}
