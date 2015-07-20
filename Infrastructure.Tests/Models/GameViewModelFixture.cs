using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrismWpfApplication.Infrastructure.Interfaces;
using Moq;
using PrismWpfApplication.Infrastructure.Models;
using System.ComponentModel;
using System.Threading;

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
            Assert.IsNotNull(target.InstallGameCommand);
            mockedNewsService.VerifyAll();
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

            //Act
            target.BackgroundImage = "";
            target.GameId = "";
            target.HeaderImage = "";
            target.HeaderText = "";

            //Verify
            Assert.IsTrue(backgroundImageChanged);
            Assert.IsTrue(gameIdChanged);
            Assert.IsTrue(headerImageChanged);
            Assert.IsTrue(headerTextChanged);
        }             
    }
}
