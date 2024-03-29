using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IFMPService _fmpService;

        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository, IFMPService fmpService)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
            _fmpService = fmpService;
        }

        [HttpGet(Name = "GetUserPortfolio")]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CrteateUserPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepository.GetStockBySymbol(symbol);
            if (stock == null)
            {
                stock = await _fmpService.FindStockBySymbolAsync(symbol);
                if (stock == null)
                {
                    return BadRequest("Cannot buy this stock because it does not exists.");
                }
                else
                {
                    await _stockRepository.CreateStock(stock);
                }
            }
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            if (userPortfolio.Any(u => u.Symbol.ToLower() == symbol.ToLower()))
            {
                return BadRequest("You already have this stock in your portfolio.");
            }
            var portfolio = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id,
            };
            var portfolioCreated = await _portfolioRepository.CreatePortfolio(portfolio);
            if (portfolioCreated == null)
            {
                return StatusCode(500, "Could not create portfolio.");
            }
            else
            {
                return CreatedAtRoute("GetUserPortfolio", null);
            }
        }

        [HttpDelete("{symbol}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserPortfolio([FromRoute] string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stocks = await _portfolioRepository.GetUserPortfolio(appUser);
            var filteredStock = stocks.Where(s => s.Symbol.ToLower() == symbol.ToLower()).FirstOrDefault();
            if (filteredStock == null)
            {
                return BadRequest("Stock is not in your portfolio.");
            }
            var portfolio = await _portfolioRepository.GetUserPortfolioByStockId(filteredStock.Id);
            var deletedPortfolio = await _portfolioRepository.DeletePortfolio(portfolio);
            return NoContent();
        }
    }
}