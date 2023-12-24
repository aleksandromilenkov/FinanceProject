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

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CommentDTO>))]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentRepository.GetAllComments();
            var commentDTO = comments.Select(c => c.ToCommentDTO());
            return Ok(commentDTO);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Comment))]
        public async Task<IActionResult> GetComment([FromRoute] int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            return Ok(comment);
        }
    }
}