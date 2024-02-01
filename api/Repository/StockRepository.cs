using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
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
        public async Task<bool> CreateStock(Stock stock)
        {
            await _context.AddAsync(stock);
            return await Save();
        }

        public async Task<bool> DeleteStock(Stock stock)
        {
            _context.Remove(stock);
            return await Save();
        }

        public async Task<Stock> GetStockById(int id)
        {
            return await _context.Stocks.Where(s => s.Id == id).Include(s => s.Comments).FirstOrDefaultAsync();
        }

        public async Task<Stock> GetStockBySymbol(string symbol)
        {
            return await _context.Stocks.Where(s => s.Symbol == symbol).Include(s => s.Comments).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Stock>> GetStocks(QueryObject query)
        {
            var stocks = _context.Stocks.Include(s => s.Comments).ThenInclude(a => a.AppUser).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));

            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<bool> UpdateStock(Stock stock)
        {
            _context.Update(stock);
            return await Save();
        }
    }
}