using PrismWpfApplication.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWpfApplication.Infrastructure.Interfaces
{
    public interface IGameService
    {
        GameViewModel[] GetGames();
    }
}
