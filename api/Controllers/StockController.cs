using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Stock;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StockController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StockDTO>))]
        public IActionResult GetStocks()
        {
            var stocks = _context.Stocks.ToList().Select(s => s.ToStockDto());
            return Ok(stocks);
        }
        [HttpGet("{id}"), ActionName("GetById")]
        [ProducesResponseType(200, Type = typeof(StockDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetStockById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(StockDTO))]
        [ProducesResponseType(400)]
        public IActionResult CreateStock([FromBody] CreateStockRequestDTO stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = stockDto.ToStockFromCreateStockDTO();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction("GetById", new { id = stockModel.Id }, stockModel.ToStockDto());
        }
    }
}