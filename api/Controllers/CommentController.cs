using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Comment;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IFMPService _fmpService;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(UserManager<AppUser> userManager, ICommentRepository commentRepository, IStockRepository stockRepository, IFMPService fmpService)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            _fmpService = fmpService;
            _userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CommentDTO>))]
        public async Task<IActionResult> GetComments([FromQuery] QueryCommentObject queryCommentObject)
        {
            var comments = await _commentRepository.GetAllComments(queryCommentObject);
            var commentDTO = comments.Select(c => c.ToCommentDTO());
            return Ok(commentDTO);
        }

        [HttpGet("{id:int}"), ActionName("GetComment")]
        [ProducesResponseType(200, Type = typeof(Comment))]
        public async Task<IActionResult> GetComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _commentRepository.CommentExists(id))
            {
                return BadRequest("No comment found with this Id");
            }
            var comment = await _commentRepository.GetByIdAsync(id);

            return Ok(comment);
        }

        [HttpPost("{symbol:alpha}")]
        public async Task<IActionResult> CreateComment([FromRoute] string symbol, CreateCommentRequestDTO comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockRepository.GetStockBySymbol(symbol);
            if (stock == null)
            {
                stock = await _fmpService.FindStockBySymbolAsync(symbol);
                if (stock == null)
                {
                    return BadRequest("Cant create comment for this stock");
                }
                await _stockRepository.CreateStock(stock);
                stock = await _stockRepository.GetStockBySymbol(symbol);
            }
            if (stock == null)
            {
                return BadRequest("Cant create comment for this stock");
            }
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            Comment comment1 = comment.ToCommentFromCreateCommentDTO(stock.Id);
            comment1.AppUserId = appUser.Id;
            if (await _commentRepository.CreateComment(comment1))
            {
                return CreatedAtAction("GetComment", new { id = comment1.Id }, comment1.ToCommentDTO());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{commentId:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int commentId, [FromBody] UpdateCommentRequestDTO updatedComment)
        {
            if (commentId < 0)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _commentRepository.CommentExists(commentId))
            {
                return BadRequest("Comment does not exists");
            }
            Comment oldComment = await _commentRepository.GetByIdAsyncAsNoTracking(commentId);
            Comment comment = updatedComment.ToCommentFromUpdateCommentDTO(oldComment.Id, (int)oldComment.StockId);
            if (await _commentRepository.UpdateComment(comment))
            {
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{commentId:int}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            if (commentId < 0)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _commentRepository.CommentExists(commentId))
            {
                return BadRequest("Comment does not exists");
            }
            Comment comment = await _commentRepository.GetByIdAsyncAsNoTracking(commentId);
            if (await _commentRepository.DeleteComment(comment))
            {
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}