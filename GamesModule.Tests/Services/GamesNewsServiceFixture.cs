using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using System.ComponentModel;
using Moq;
using PrismWpfApplication.Modules.GamesModule.Services;
using System.Threading.Tasks;
using PrismWpfApplication.Infrastructure.Models;

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

        [TestMethod]
        public async Task WhenGetNewsAsyncCalled_GetValues()
        {
            //Prepare
            GamesNewsService service = new GamesNewsService();

            // Act
            Article[] result = await service.GetNewsAsync(new string[] { "Diablo" });
            Article[] result2 = await service.GetNewsAsync(new string[] { "Faked" });
            Article[] result3 = await service.GetNewsAsync(null);

            //Verify
            Assert.IsNotNull(result[0].ArticleType);
            Assert.IsNotNull(result[0].Content);
            Assert.IsNotNull(result[0].Keywords);
            Assert.IsNotNull(result[0].Title);
            Assert.AreEqual(0, result2.Length);
            Assert.IsNull(result3);
        }
    }
}
