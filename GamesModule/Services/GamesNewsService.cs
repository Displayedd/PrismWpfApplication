using PrismWpfApplication.Infrastructure.Interfaces;
using PrismWpfApplication.Infrastructure.Models;
using PrismWpfApplication.Modules.GamesModule.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrismWpfApplication.Modules.GamesModule.Services
{
    public class GamesNewsService : INewsService
    {
        private List<Article> _articles;

        /// <summary>
        /// Get news associated with the sumbitted keywords.
        /// </summary>
        /// <param name="keywords">Array of strings to filter news by.</param>
        /// <returns>Array of Articles.</returns>
        public Article[] GetNews(string[] keywords)
        {
            if(keywords == null)
                return null;

            var document = XDocument.Parse(Resources.NewsArticles);
            var articles = from x in document.Descendants("Article").AsParallel()
                           where XElementsToStringArray(x.Element("Keywords").Descendants()).Intersect(keywords).Any()
                           select new Article
                           {
                               ArticleType = GenerateArticleTypeFromString(x.Element("ArticleType").Value),
                               Title = x.Element("Title").Value,
                               Content = x.Element("Content").Value,
                               Image = GenerateImageFromBase64String(x.Element("Image").Value),
                               Keywords = XElementsToStringArray(x.Element("Keywords").Descendants())
                           };

            return articles.ToArray();
        }

        /// <summary>
        /// Get news associated with the sumbitted keywords.
        /// </summary>
        /// <param name="keywords">Array of strings to filter news by.</param>
        /// <returns>Array of Articles.</returns>
        public async Task<Article[]> GetNewsAsync(string[] keywords, CancellationToken token = new CancellationToken())
        {
            if (keywords == null)
                return null;
            // Simulate long operation.
            await Task.Delay(TimeSpan.FromSeconds(2), token).ConfigureAwait(false);
            var document = XDocument.Parse(Resources.NewsArticles);
            var articles = await Task.Run(() => from x in document.Descendants("Article").AsParallel().WithCancellation(token)
                           where XElementsToStringArray(x.Element("Keywords").Descendants()).Intersect(keywords).Any()
                           select new Article
                           {
                               ArticleType = GenerateArticleTypeFromString(x.Element("ArticleType").Value),
                               Title = x.Element("Title").Value,
                               Content = x.Element("Content").Value,
                               Image = GenerateImageFromBase64String(x.Element("Image").Value),
                               Keywords = XElementsToStringArray(x.Element("Keywords").Descendants())
                           }, token);
            return articles.ToArray();
        }

        private void InitializeService()
        {
            var document = XDocument.Parse(Resources.NewsArticles);
            var articles = from x in document.Descendants("Article").AsParallel()
                           select new Article
                           {
                               ArticleType = GenerateArticleTypeFromString(x.Element("ArticleType").Value),
                               Title = x.Element("Title").Value,
                               Content = x.Element("Content").Value,
                               Image = GenerateImageFromBase64String(x.Element("Image").Value),
                               Keywords = XElementsToStringArray(x.Element("Keywords").Descendants())
                           };

            _articles = articles.ToList();
        }

        private Image GenerateImageFromBase64String(string data)
        {
            Image image;
            if (string.IsNullOrEmpty(data))
                return null;
            try
            {
                using (Stream stream = GenerateStreamFromString(data))
                {
                    image = Image.FromStream(stream);
                }
                return image;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private Stream GenerateStreamFromString(string s)
        {
            var bytes = Convert.FromBase64String(s);
            MemoryStream stream = new MemoryStream(bytes);
            return stream;
        }

        private string[] XElementsToStringArray(IEnumerable<XElement> elements)
        {
            List<string> values = new List<string>();
            foreach (XElement element in elements)
            {
                values.Add(element.Value);
            }
            return values.ToArray();
        }

        private ArticleTypes GenerateArticleTypeFromString(string data)
        {
            ArticleTypes articleType = ArticleTypes.Minor;
            try
            {
                articleType = (ArticleTypes)Enum.Parse(typeof(ArticleTypes), data);
            }
            catch (Exception ex)
            {

            }

            return articleType;
        }
    }
}
