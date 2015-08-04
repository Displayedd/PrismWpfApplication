using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrismWpfApplication.Infrastructure.Interfaces;
using Moq;
using PrismWpfApplication.Infrastructure.Models;
using System.ComponentModel;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Input;

namespace Infrastructure.Tests.Models
{
    [TestClass]
    public class GameViewModelFixture
    {
        [TestMethod]
        public void WhenConstructed_IntializesValues()
        {
            //Prepare 
            Mock<INewsService> mockedNewsService = new Mock<INewsService>();
            Mock<IUserService> mockedUserService = new Mock<IUserService>();

            //Act
            GameViewModel target = new GameViewModel(mockedNewsService.Object, mockedUserService.Object);

            //Verify
            Assert.IsNull(target.BackgroundImage);
            Assert.IsNull(target.GameId);
            Assert.IsNull(target.HeaderImage);
            Assert.IsNull(target.HeaderText);
            Assert.IsNotNull(target.InstallGameCommand);
            Assert.IsInstanceOfType(target.SelectGameRegionCommand, typeof(ICommand));
            mockedNewsService.VerifyAll();
        }

        [TestMethod]
        public void WhenPropertyChanged_PropertyIsUpdated()
        {
            //Prepare 
            Mock<INewsService> mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(x => x.GetNews(It.Is<string[]>(keywords => keywords.Length > 0))).Returns(
                new Article[] { new Article() }).Verifiable();

            GameRegion[] gameRegions = new GameRegion[] { 
                new GameRegion { Header = "Asia & Southeast Asia"}, 
                new GameRegion { Header = "Europe"},
                new GameRegion { Header = "Asia"},
                new GameRegion { Header = "China"}};
            Mock<IUserService> mockedUserService = new Mock<IUserService>();
            mockedUserService.SetupGet(x => x.HomeRegion).Returns(gameRegions[1]);
            mockedUserService.SetupGet(x => x.GameRegions).Returns(gameRegions);

            GameViewModel target = new GameViewModel(mockedNewsService.Object, mockedUserService.Object);

            bool backgroundImageChanged = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "BackgroundImage")
                {
                    backgroundImageChanged = true;
                }
            };

            bool gameIdChanged = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "GameId")
                {
                    gameIdChanged = true;
                }
            };

            bool headerImageChanged = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "HeaderImage")
                {
                    headerImageChanged = true;
                }
            };

            bool headerTextChanged = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "HeaderText")
                {
                    headerTextChanged = true;
                }
            };

            bool selectedRegionChanged = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "SelectedRegion")
                {
                    selectedRegionChanged = true;
                }
            };

            bool gameRegionsChanged = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "GameRegions")
                {
                    gameRegionsChanged = true;
                }
            };

            //Act
            target.BackgroundImage = "";
            target.GameId = "";
            target.HeaderImage = "";
            target.HeaderText = "";
            target.SelectedRegion = gameRegions[2];
            target.GameRegions = null;

            //Verify
            Assert.IsTrue(backgroundImageChanged);
            Assert.IsTrue(gameIdChanged);
            Assert.IsTrue(headerImageChanged);
            Assert.IsTrue(headerTextChanged);
            Assert.IsTrue(selectedRegionChanged);
            Assert.IsTrue(gameRegionsChanged);
        }

        [TestMethod]
        public void WhenSelectGameRegionChanged_HomeRegionUpdated()
        {
            //Prepare
            Mock<INewsService> mockedNewsService = new Mock<INewsService>();
            GameRegion[] gameRegions = new GameRegion[] { 
                new GameRegion { Header = "Asia & Southeast Asia"}, 
                new GameRegion { Header = "Europe"},
                new GameRegion { Header = "Asia"},
                new GameRegion { Header = "China"}};
            Mock<IUserService> mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(x => x.GameRegions).Returns(gameRegions);
            mockedUserService.SetupSet(x => x.HomeRegion = gameRegions[0]).Verifiable();
            mockedUserService.SetupGet(x => x.HomeRegion).Returns(gameRegions[1]);

            GameViewModel target = new GameViewModel(mockedNewsService.Object, mockedUserService.Object);

            //Act
            target.SelectedRegion = gameRegions[0];

            //Verify
            Assert.AreEqual(gameRegions[0], target.SelectedRegion);
            Assert.IsInstanceOfType(target.GameRegions, typeof(List<GameRegion>));
            mockedUserService.VerifySet(x => x.HomeRegion = gameRegions[0]);
        }

        [TestMethod]
        public void WhenSelectGameRegionCommandExecuted_SelectedRegionPropertyChanged()
        {
            //Prepare
            Mock<INewsService> mockedNewsService = new Mock<INewsService>();
            GameRegion[] gameRegions = new GameRegion[] { 
                new GameRegion { Header = "Asia & Southeast Asia"}, 
                new GameRegion { Header = "Europe"},
                new GameRegion { Header = "Asia"},
                new GameRegion { Header = "China"}};
            Mock<IUserService> mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(x => x.GameRegions).Returns(gameRegions);
            mockedUserService.SetupSet(x => x.HomeRegion = gameRegions[0]).Verifiable();
            mockedUserService.SetupGet(x => x.HomeRegion).Returns(gameRegions[1]);

            GameViewModel target = new GameViewModel(mockedNewsService.Object, mockedUserService.Object);

            GameRegion gameRegion = new GameRegion();

            //Act
            target.SelectGameRegionCommand.Execute(gameRegion);

            //Verify
            Assert.AreEqual(gameRegion, target.SelectedRegion);
        }
    }
}
