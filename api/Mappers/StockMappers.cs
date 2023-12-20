using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDTO ToStockDto(this Stock stockModel)
        {
            return new StockDTO
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                LastDiv = stockModel.LastDiv,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                MarketCup = stockModel.MarketCup,
                Industry = stockModel.Industry,
            };
        }
    }
}