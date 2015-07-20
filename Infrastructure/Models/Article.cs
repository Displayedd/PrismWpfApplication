using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWpfApplication.Infrastructure.Models
{
    public class Article
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Image Image { get; set; }
        public string[] Keywords { get; set; }
        public ArticleTypes ArticleType { get; set; }
    }
}
