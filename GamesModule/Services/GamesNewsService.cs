﻿using PrismWpfApplication.Infrastructure.Interfaces;
using PrismWpfApplication.Infrastructure.Models;
using PrismWpfApplication.Modules.GamesModule.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrismWpfApplication.Modules.GamesModule.Services
{
    public class GamesNewsService : INewsService
    {
        private List<Article> _articles;

        public GamesNewsService()
        {
            this.InitializeService();
        }

        public Article[] GetNews(string[] keywords)
        {
            if(keywords == null)
                return null;
            var articles = from x in _articles
                           where x.Keywords.Intersect(keywords).Any()
                           select x;
            return articles.ToArray();
        }

        private void InitializeService()
        {
            var document = XDocument.Parse(Resources.NewsArticles);
            var articles = from x in document.Descendants("Article")
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
