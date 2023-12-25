using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Comment;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllComments();
        Task<Comment> GetByIdAsync(int id);
        Task<bool> CreateComment(Comment comment);
        Task<bool> Save();
    }
}