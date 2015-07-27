using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWpfApplication.Infrastructure.Models
{
    public class Article : IDisposable
    {
        private bool disposed = false;
        private Image image;

        public string Title { get; set; }
        public string Content { get; set; }
        public Image Image
        {
            get
            {
                if (!disposed)
                    return this.image;
                else
                    throw new ObjectDisposedException("Image");
            }
            set { this.image = value; }
        }
        public string[] Keywords { get; set; }
        public ArticleTypes ArticleType { get; set; }

        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (Image != null)
                    {
                        try
                        {
                            Image.Dispose();
                            Image = null;
                        }
                        catch (ObjectDisposedException)
                        {

                        }
                    }                        
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
