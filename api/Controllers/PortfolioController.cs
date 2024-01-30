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

        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
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

        [HttpPost("{symbol}")]
        [Authorize]
        public async Task<IActionResult> CrteateUserPortfolio([FromRoute] string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepository.GetStocks(new QueryObject { Symbol = symbol });
            if (stock == null)
            {
                return BadRequest();
            }
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            if (userPortfolio.Any(u => u.Symbol.ToLower() == symbol.ToLower()))
            {
                return BadRequest("You already have this stock in your portfolio.");
            }
            var portfolio = new Portfolio
            {
                StockId = stock.FirstOrDefault().Id,
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

    }
}