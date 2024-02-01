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
                Comments = stockModel.Comments.Select(s => s.ToCommentDTO()).ToList()
            };
        }



        public static Stock ToStockFromCreateStockDTO(this CreateStockRequestDTO stockDTO)
        {
            return new Stock
            {
                Symbol = stockDTO.Symbol,
                LastDiv = stockDTO.LastDiv,
                CompanyName = stockDTO.CompanyName,
                Purchase = stockDTO.Purchase,
                MarketCup = stockDTO.MarketCup,
                Industry = stockDTO.Industry,
            };
        }

        public static Stock ToStockFromUpdateStockDTO(this UpdateStockRequestDTO stockDTO)
        {
            return new Stock
            {
                Id = stockDTO.Id,
                Symbol = stockDTO.Symbol,
                LastDiv = stockDTO.LastDiv,
                CompanyName = stockDTO.CompanyName,
                Purchase = stockDTO.Purchase,
                MarketCup = stockDTO.MarketCup,
                Industry = stockDTO.Industry,
            };
        }

        public static Stock ToStockFromFMP(this FMPStock fmpStock)
        {
            return new Stock
            {
                Symbol = fmpStock.symbol,
                LastDiv = (decimal)fmpStock.lastDiv,
                CompanyName = fmpStock.companyName,
                Purchase = (decimal)fmpStock.price,
                MarketCup = fmpStock.mktCap,
                Industry = fmpStock.industry,
            };
        }

    }
}