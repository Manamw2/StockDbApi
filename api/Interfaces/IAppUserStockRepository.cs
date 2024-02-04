using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IAppUserStockRepository
    {
        Task<List<Stock>> GetUserStocks(AppUser user);
        Task<AppUserStock?> AddUserStockAsync(AppUser user, int stockId);
        Task<AppUserStock?> RemoveUserStockAsync(AppUser user, int stockId);
    }
}