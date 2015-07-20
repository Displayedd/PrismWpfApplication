using PrismWpfApplication.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PrismWpfApplication.Infrastructure.Converters
{
    public class ArticleTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MajorArticleTemplate { get; set; }
        public DataTemplate MinorArticleTemplate { get; set; }
        public DataTemplate NotificationTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Article article = item as Article;
            if (article != null)
            {
                switch (article.ArticleType)
                {
                    case ArticleTypes.Major: return MajorArticleTemplate;
                    case ArticleTypes.Minor: return MinorArticleTemplate;
                    case ArticleTypes.Notification: return NotificationTemplate;
                    default: return null;
                }
            }
            else
                return null;
        }
    }
}
