using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrismWpfApplication.Infrastructure.Converters;
using System.Windows;
using PrismWpfApplication.Infrastructure.Models;

namespace Infrastructure.Tests.Converters
{
    [TestClass]
    public class ArticleTemplateSelectorFixture
    {
        [TestMethod]
        public void ShouldSelectDataTemplate()
        {
            // Prepare
            ArticleTemplateSelector selector = new ArticleTemplateSelector();
            selector.MajorArticleTemplate = new DataTemplate("major");
            selector.MinorArticleTemplate = new DataTemplate("minor");
            selector.NotificationTemplate = new DataTemplate("notification");

            object majorArticle = new Article { ArticleType = ArticleTypes.Major };
            object minorArticle = new Article { ArticleType = ArticleTypes.Minor };
            object notificationArticle = new Article { ArticleType = ArticleTypes.Notification };
            object unknownArticle = new Article ();

            //Act
            DataTemplate majorTemplate = selector.SelectTemplate(majorArticle, null) as DataTemplate;
            DataTemplate minorTemplate = selector.SelectTemplate(minorArticle, null) as DataTemplate;
            DataTemplate notificationTemplate = selector.SelectTemplate(notificationArticle, null) as DataTemplate;
            DataTemplate unknownTemplate = selector.SelectTemplate(unknownArticle, null) as DataTemplate;

            //Verify
            Assert.AreEqual("major", majorTemplate.DataType);
            Assert.AreEqual("minor", minorTemplate.DataType);
            Assert.AreEqual("notification", notificationTemplate.DataType);
            Assert.IsNotNull(unknownTemplate.DataType);
        }
    }
}
