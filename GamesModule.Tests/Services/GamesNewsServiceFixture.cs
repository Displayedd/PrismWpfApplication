using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using System.ComponentModel;
using Moq;
using PrismWpfApplication.Modules.GamesModule.Services;

namespace GamesModule.Tests.Services
{
    [TestClass]
    public class GamesNewsServiceFixture
    {
        [TestMethod]
        public void WhenConstructed_IntializesValues()
        {
            //Act
            GamesNewsService service = new GamesNewsService();

            //Verify
            Assert.IsNotNull(service.GetNews(new string[] { "Diablo" })[0].ArticleType);
            Assert.IsNotNull(service.GetNews(new string[] { "Diablo" })[0].Content);
            Assert.IsNotNull(service.GetNews(new string[] { "Diablo" })[0].Keywords);
            Assert.IsNotNull(service.GetNews(new string[] { "Diablo" })[0].Title);
            Assert.AreEqual(0, service.GetNews(new string[] { "Faked" }).Length);
            Assert.IsNull(service.GetNews(null));
        }
    }
}
