using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> Create(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public Task<Comment> Create(Comment comment, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Comment?> Delete(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if(comment == null){
                return null;
            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetById(int id)
        {
            var Comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if(Comment == null){
                return null;
            }
            return Comment;
        }

        public async Task<Comment?> Update(Comment updateComment, int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if(comment == null){
                return null;
            }
            comment.Title = updateComment.Title;
            comment.Content = updateComment.Content;
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}