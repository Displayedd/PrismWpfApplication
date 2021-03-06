﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrismWpfApplication.Infrastructure.Interfaces;
using Moq;
using PrismWpfApplication.Infrastructure.Models;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using PrismWpfApplication.Infrastructure;

namespace Infrastructure.Tests.Models
{
    [TestClass]
    public class BaseArticleViewModelFixture
    {
        [TestMethod]
        public void WhenConstructed_IntializesValues()
        {
            //Prepare 
            Mock<INewsService> mockedNewsService = new Mock<INewsService>();

            //Act
            BaseArticleViewModel target = new BaseArticleViewModel(mockedNewsService.Object);

            //Verify
            Assert.IsNull(target.Keywords);
            Assert.IsNull(target.MajorArticles);
            Assert.IsNull(target.MinorArticles);
            Assert.AreEqual(LoadingStates.NormalState, target.State);
            mockedNewsService.VerifyAll();
        }

        [TestMethod]
        public void WhenPropertyChanged_PropertyIsUpdated()
        {
            //Prepare 
            Mock<INewsService> mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(x => x.GetNews(It.Is<string[]>(keywords => keywords.Length > 0))).Returns(
                new Article[] { new Article() }).Verifiable();

            BaseArticleViewModel target = new BaseArticleViewModel(mockedNewsService.Object);

            bool majorArticlesChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "MajorArticles")
                {
                    majorArticlesChangedRaised = true;
                }
            };

            bool minorArticlesChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "MinorArticles")
                {
                    minorArticlesChangedRaised = true;
                }
            };

            bool stateChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "State")
                {
                    stateChangedRaised = true;
                }
            };

            //Act
            target.MajorArticles = new Article[] { new Article() };
            target.MinorArticles = new Article[] { new Article() };
            target.State = "";

            //Verify
            Assert.IsTrue(majorArticlesChangedRaised);
            Assert.IsTrue(minorArticlesChangedRaised);
            Assert.IsTrue(stateChangedRaised);
        }

        [TestMethod]
        public void WhenArticlesInitialized_ArticlesSet()
        {
            //Prepare 
            Mock<INewsService> mockedNewsService = new Mock<INewsService>();
            Article[] articles = new Article[] { 
                new Article { ArticleType = ArticleTypes.Major, Keywords = new string[] { "Diablo" } },
                new Article { ArticleType = ArticleTypes.Notification, Keywords = new string[] { "Maintenance" } }
            };

            mockedNewsService.Setup(x => x.GetNews(It.Is<string[]>(keywords => keywords.Length > 0))).Returns(articles);

            BaseArticleViewModel target = new BaseArticleViewModel(mockedNewsService.Object);

            //Act
            target.InitializeArticles(new string[] { "Diablo", "Maintenance" });

            //Verify
            Assert.IsNotNull(target.MajorArticles);
            Assert.IsNotNull(target.MinorArticles);
            mockedNewsService.VerifyAll();
        }

        [TestMethod]
        public void WhenArticlesInitializedUsingProperty_ArticlesSet()
        {
            //Prepare 
            Mock<INewsService> mockedNewsService = new Mock<INewsService>();
            Article[] articles = new Article[] { 
                new Article { ArticleType = ArticleTypes.Major, Keywords = new string[] { "Diablo" } },
                new Article { ArticleType = ArticleTypes.Notification, Keywords = new string[] { "Maintenance" } }
            };

            mockedNewsService.Setup(x => x.GetNews(It.Is<string[]>(keywords => keywords.Length > 0))).Returns(articles);

            BaseArticleViewModel target = new BaseArticleViewModel(mockedNewsService.Object);
            target.Keywords = new string[] { "Diablo", "Maintenance" };

            //Act
            target.InitializeArticles();

            //Verify
            Assert.AreEqual(1, target.MajorArticles.Count);
            Assert.AreEqual(1, target.MinorArticles.Count);
            mockedNewsService.VerifyAll();
        }

        [TestMethod]
        public async Task WhenArticlesInitializedAsync_ArticlesSet()
        {
            //Prepare 
            Mock<INewsService> mockedNewsService = new Mock<INewsService>();
            Article[] articles = new Article[] { 
                new Article { ArticleType = ArticleTypes.Major, Keywords = new string[] { "Diablo" } },
                new Article { ArticleType = ArticleTypes.Notification, Keywords = new string[] { "Maintenance" } }
            };

            mockedNewsService.Setup(x => x.GetNewsAsync(It.Is<string[]>(keywords => keywords.Length > 0), It.IsAny<CancellationToken>())).Returns(Task.FromResult(articles));

            BaseArticleViewModel target = new BaseArticleViewModel(mockedNewsService.Object);
            target.Keywords = new string[] { "Diablo", "Maintenance" };

            //Act
            await target.InitializeArticlesAsync();

            //Verify
            Assert.AreEqual(1, target.MajorArticles.Count);
            Assert.AreEqual(1, target.MinorArticles.Count);
        }

        [TestMethod]
        public void WhenCallingDisposeArticles_ArticlesDisposed()
        {
            //Prepare 
            Mock<INewsService> mockedNewsService = new Mock<INewsService>();
            Article[] articles = new Article[] { 
                new Article { ArticleType = ArticleTypes.Major, Keywords = new string[] { "Diablo" }, Image = new Bitmap(10,10) },
                new Article { ArticleType = ArticleTypes.Notification, Keywords = new string[] { "Maintenance" }, Image = new Bitmap(10,10) }
            };

            mockedNewsService.Setup(x => x.GetNews(It.Is<string[]>(keywords => keywords.Length > 0))).Returns(articles);

            BaseArticleViewModel target = new BaseArticleViewModel(mockedNewsService.Object);

            //Act
            target.InitializeArticles(new string[] { "Diablo", "Maintenance" });
            target.DisposeArticles();

            //Verify
            Assert.IsNull(target.MajorArticles);
            Assert.IsNull(target.MinorArticles);
            mockedNewsService.VerifyAll();
        }
    }
}
