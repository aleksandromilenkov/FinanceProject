using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Comment;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CommentExists(int id)
        {
            return await _context.Comments.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> CreateComment(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            return await Save();
        }

        public async Task<bool> DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
            return await Save();
        }

        public async Task<List<Comment>> GetAllComments()
        {
            return await _context.Comments.Include(c => c.AppUser).ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Comments.Where(c => c.Id == id).Include(c => c.AppUser).FirstOrDefaultAsync();
        }

        public async Task<Comment> GetByIdAsyncAsNoTracking(int id)
        {
            return await _context.Comments.Where(c => c.Id == id).Include(c => c.AppUser).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateComment(Comment comment)
        {
            _context.Comments.Update(comment);
            return await Save();
        }
    }
}