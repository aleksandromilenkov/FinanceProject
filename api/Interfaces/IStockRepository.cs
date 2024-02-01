using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<ICollection<Stock>> GetStocks(QueryObject query);
        Task<Stock> GetStockById(int id);
        Task<Stock> GetStockBySymbol(string symbol);
        Task<bool> StockExists(int id);
        Task<bool> CreateStock(Stock stock);
        Task<bool> UpdateStock(Stock stock);
        Task<bool> DeleteStock(Stock stock);
        Task<bool> Save();
    }
}