using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Stock;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(StockDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDTO stockDto)
        {
            if (id != stockDto.Id)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockToUpdate = _context.Stocks.AsNoTracking().Where(s => s.Id == id).FirstOrDefault();
            if (stockToUpdate == null)
            {
                return NotFound();
            }
            Stock toStock = stockDto.ToStockFromUpdateStockDTO();
            _context.Stocks.Update(toStock);
            if (_context.SaveChanges() <= 0)
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteStock([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = _context.Stocks.AsNoTracking().Where(s => s.Id == id).FirstOrDefault();
            if (stock == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stock);
            if (_context.SaveChanges() <= 0)
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}