using PrismWpfApplication.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrismWpfApplication.Infrastructure.Interfaces
{
    public interface INewsService
    {
        Article[] GetNews(string[] keywords);
        Task<Article[]> GetNewsAsync(string[] keywords, CancellationToken token = new CancellationToken());
    }
}
