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

            //Act
            GameViewModel target = new GameViewModel(mockedNewsService.Object);

            //Verify
            Assert.IsNull(target.BackgroundImage);
            Assert.IsNull(target.GameId);
            Assert.IsNull(target.HeaderImage);
            Assert.IsNull(target.HeaderText);
            Assert.IsNull(target.LogoImage);
            Assert.IsNotNull(target.InstallGameCommand);
            Assert.IsNull(target.GameRegions);
            Assert.IsNull(target.SelectedRegion);
            Assert.IsInstanceOfType(target.SelectGameRegionCommand, typeof(ICommand));
        }

        [TestMethod]
        public void WhenPropertyChanged_PropertyIsUpdated()
        {
            //Prepare 
            Mock<INewsService> mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(x => x.GetNews(It.Is<string[]>(keywords => keywords.Length > 0))).Returns(
                new Article[] { new Article() }).Verifiable();

            GameViewModel target = new GameViewModel(mockedNewsService.Object);

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

            bool logoImageChanged = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "LogoImage")
                {
                    logoImageChanged = true;
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
            target.LogoImage = "";
            target.SelectedRegion = new GameRegion();
            target.GameRegions = new GameRegion[] { };

            //Verify
            Assert.IsTrue(backgroundImageChanged);
            Assert.IsTrue(gameIdChanged);
            Assert.IsTrue(headerImageChanged);
            Assert.IsTrue(headerTextChanged);
            Assert.IsTrue(logoImageChanged);
            Assert.IsTrue(selectedRegionChanged);
            Assert.IsTrue(gameRegionsChanged);
        }

        [TestMethod]
        public void WhenSelectGameRegionCommandExecuted_SelectedRegionPropertyChanged()
        {
            //Prepare
            Mock<INewsService> mockedNewsService = new Mock<INewsService>();
            GameViewModel target = new GameViewModel(mockedNewsService.Object);
            GameRegion gameRegion = new GameRegion();

            //Act
            target.SelectGameRegionCommand.Execute(gameRegion);

            //Verify
            Assert.AreEqual(gameRegion, target.SelectedRegion);
        }

        [TestMethod]
        public void WhenGameRegionsChanged_SelectedRegionPropertyUpdated()
        {
            //Prepare
            Mock<INewsService> mockedNewsService = new Mock<INewsService>();
            GameViewModel target = new GameViewModel(mockedNewsService.Object);
            List<GameRegion> gameRegions = new List<GameRegion> { new GameRegion(), new GameRegion() };

            //Act
            target.GameRegions = gameRegions;

            //Verify
            Assert.AreEqual(gameRegions[0], target.SelectedRegion);
        }
    }
}
