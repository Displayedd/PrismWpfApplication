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

            //Act
            target.MajorArticles = new Article[] { new Article() };
            target.MinorArticles = new Article[] { new Article() };

            //Verify
            Assert.IsTrue(majorArticlesChangedRaised);
            Assert.IsTrue(minorArticlesChangedRaised);
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
            Assert.IsNotNull(target.MajorArticles);
            Assert.IsNotNull(target.MinorArticles);
            mockedNewsService.VerifyAll();
        }  
    }
}
