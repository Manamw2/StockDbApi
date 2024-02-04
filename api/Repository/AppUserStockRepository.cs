using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class AppUserStockRepository : IAppUserStockRepository
    {
        private readonly ApplicationDbContext _Context;
        public AppUserStockRepository(ApplicationDbContext context)
        {
            _Context = context;
        }

        public async Task<AppUserStock?> AddUserStockAsync(AppUser user, int stockId)
        {
            var appuserStock = new AppUserStock{
                AppUserId = user.Id,
                StockId = stockId
            };
            await _Context.AppUserStocks.AddAsync(appuserStock);
            await _Context.SaveChangesAsync();
            return appuserStock;
        }

        public async Task<List<Stock>> GetUserStocks(AppUser user)
        {
            return await _Context.AppUserStocks.Where(x => x.AppUserId == user.Id)
                .Select(y => new Stock{
                    Id = y.StockId,
                    Sympol = y.Stock.Sympol,
                    CompanyName = y.Stock.CompanyName,
                    Purchase = y.Stock.Purchase,
                    Industry = y.Stock.Industry,
                    LastDiv = y.Stock.LastDiv,
                    MarketCap = y.Stock.MarketCap
                }).ToListAsync();
        }

        public async Task<AppUserStock?> RemoveUserStockAsync(AppUser user, int stockId)
        {
            var appuserStock = await _Context.AppUserStocks.FirstOrDefaultAsync(x => x.AppUser.Id == user.Id && x.StockId == stockId);
            if(appuserStock == null){
                return null;
            }
            _Context.AppUserStocks.Remove(appuserStock);
            await _Context.SaveChangesAsync();
            return appuserStock;
        }
    }
}