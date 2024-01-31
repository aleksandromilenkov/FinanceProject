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
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;

        public PortfolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> GetUserPortfolioByUserId(string userId)
        {
            return await _context.Portfolios.Where(p => p.AppUserId == userId).FirstOrDefaultAsync();
        }

        public async Task<Portfolio> GetUserPortfolioByStockId(int stockId)
        {
            return await _context.Portfolios.Where(p => p.StockId == stockId).FirstOrDefaultAsync();
        }

        public async Task<Portfolio> CreatePortfolio(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _context.Portfolios.Where(p => p.AppUserId == user.Id).Select(p => p.Stock).ToListAsync();
        }

        public async Task<Portfolio> DeletePortfolio(Portfolio portfolio)
        {
            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }
    }
}