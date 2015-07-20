﻿using Microsoft.Practices.Prism.Mvvm;
using PrismWpfApplication.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace PrismWpfApplication.Infrastructure.Models
{
    public class BaseArticleViewModel : BindableBase
    {
        private IList<Article> minorArticles;
        private IList<Article> majorArticles;
        private readonly INewsService newService;
        public string[] Keywords { get; set; }

        public BaseArticleViewModel(INewsService newService)
        {
            if (newService == null)
                throw new ArgumentNullException("newsService");

            this.newService = newService;
        }

        #region Properties
        public IList<Article> MinorArticles
        {
            get { return this.minorArticles; }
            set
            {
                SetProperty(ref this.minorArticles, value);
            }
        }

        public IList<Article> MajorArticles
        {
            get { return this.majorArticles; }
            set
            {
                SetProperty(ref this.majorArticles, value);
            }
        }
        #endregion
        
        /// <summary>
        /// Retrieves articles from the associated INewService
        /// based on the submitted keywords.
        /// </summary>
        /// <param name="keywords">Filter</param>
        public void InitializeArticles(string[] keywords)
        {
            this.GetArticles(keywords);
        }

        /// <summary>
        /// Retrieves articles from the associated INewService
        /// based on the objects Keywords property.
        /// </summary>
        public void InitializeArticles()
        {
            if (this.Keywords != null && this.Keywords.Length != 0)
                this.GetArticles(this.Keywords);
        }

        protected virtual void GetArticles(string[] keywords)
        {
            Article[] articles = this.newService.GetNews(keywords);

            this.MajorArticles = (from major in articles
                                  where major.ArticleType == ArticleTypes.Major
                                  select major).ToList();

            this.MinorArticles = (from minor in articles
                                  where minor.ArticleType == ArticleTypes.Minor ||
                                  minor.ArticleType == ArticleTypes.Notification
                                  select minor).ToList();
        }
    }
}