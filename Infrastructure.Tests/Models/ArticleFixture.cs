using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrismWpfApplication.Infrastructure.Interfaces;
using Moq;
using PrismWpfApplication.Infrastructure.Models;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;

namespace Infrastructure.Tests.Models
{
    [TestClass]
    public class ArticleFixture
    {
        [TestMethod]
        public void WhenConstructed_IntializesValues()
        {
            //Act
            Article target = new Article
            {
                ArticleType = ArticleTypes.Major,
                Image = new Bitmap(10, 10),
                Content = "",
                Keywords = new string[] { "" },
                Title = ""
            };

            //Verify
            Assert.AreEqual(ArticleTypes.Major, target.ArticleType);
            Assert.AreEqual(string.Empty, target.Content);
            Assert.IsNotNull(target.Image);
            Assert.IsNotNull(target.Keywords);
            Assert.AreEqual(string.Empty, target.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException), "Image")]
        public void WhenDisposed_ImageDisposed()
        {
            //Prepare 
            Article target = new Article { Image = new Bitmap(10, 10) };

            //Act
            target.Dispose();

            //Verify
            Image image = target.Image;
        }
    }
}
