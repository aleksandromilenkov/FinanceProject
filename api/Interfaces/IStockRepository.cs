using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<ICollection<Stock>> GetStocks();
        Task<Stock> GetStockById(int id);
        Task<bool> StockExists(int id);
        Task<bool> CreateStock(Stock stock);
        Task<bool> UpdateStock(Stock stock);
        Task<bool> DeleteStock(Stock stock);
        Task<bool> Save();
    }
}