using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CommentDTO>))]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentRepository.GetAllComments();
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

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId, CreateCommentRequestDTO comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _stockRepository.StockExists(stockId))
            {
                return BadRequest("Stock does not exists");
            }
            Comment comment1 = comment.ToCommentFromCreateCommentDTO(stockId);
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