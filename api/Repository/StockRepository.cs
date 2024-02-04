using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if(stock == null){
                return null;
            }
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<List<Stock>> GetAllAsync(GetStocksObject getStocksObject)
        {
            var stocks =  _context.Stocks.Include(s => s.Comments).AsQueryable();
            if(!string.IsNullOrWhiteSpace(getStocksObject.Industry)){
                stocks = stocks.Where(s => s.Industry.Equals(getStocksObject.Industry));
            }

            return await stocks.Skip((getStocksObject.PageNumber - 1) * getStocksObject.PageSize).Take(getStocksObject.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public  async Task<bool> StockExist(int id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockDto stock)
        {
            var stock1 = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if(stock1 == null){
                return null;
            }
            stock1.Sympol = stock.Sympol;
            stock1.CompanyName = stock.CompanyName;
            stock1.Purchase = stock.Purchase;
            stock1.LastDiv = stock.LastDiv;
            stock1.Industry = stock.Industry;
            stock1.MarketCap = stock.MarketCap;
            await _context.SaveChangesAsync();
            return stock1;
        }
    }
}