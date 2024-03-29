using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetById(int id);
        
        Task<Comment> Create(Comment comment);

        Task<Comment?> Update(Comment comment, int id);
        Task<Comment?> Delete(int id);
    }
}