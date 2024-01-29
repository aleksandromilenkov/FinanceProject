using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;

        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StockDTO>))]
        [Authorize]
        public async Task<IActionResult> GetStocks([FromQuery] QueryObject query)
        {
            var stocks = await _stockRepository.GetStocks(query);
            var stocksDto = stocks.Select(s => s.ToStockDto());
            return Ok(stocksDto);
        }
        [HttpGet("{id:int}"), ActionName("GetById")]
        [ProducesResponseType(200, Type = typeof(StockDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock = await _stockRepository.GetStockById(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(StockDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDTO stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = stockDto.ToStockFromCreateStockDTO();
            if (await _stockRepository.CreateStock(stockModel))
            {
                return CreatedAtAction("GetById", new { id = stockModel.Id }, stockModel.ToStockDto());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(200, Type = typeof(StockDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDTO stockDto)
        {
            if (id != stockDto.Id)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _stockRepository.StockExists(id))
            {
                return NotFound();
            }
            Stock toStock = stockDto.ToStockFromUpdateStockDTO();

            if (!await _stockRepository.UpdateStock(toStock))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _stockRepository.StockExists(id))
            {
                return NotFound();
            }
            Stock stock = await _stockRepository.GetStockById(id);
            if (!await _stockRepository.DeleteStock(stock))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}