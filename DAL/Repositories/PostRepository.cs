using DAL.Context;
using DAL.Enteties;
using DAL.Interfaces.BaseInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    /// Class that represents a message repository
    /// </summary>
    public class PostRepository : IRepository<Post>
    {
        private readonly SiteContext _context;

        /// <summary>
        /// Constructor for creating a repository with given db context
        /// </summary>
        /// <param name="context">The instance of db context for the repository</param>
        public PostRepository(SiteContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Post item)
        {
            await _context.Posts.AddAsync(item);
        }

        public void Delete(int id)
        {
            var post = _context.Posts.Find(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }
        }

        public async Task<IQueryable<Post>> GetAllAsync()
        {
            var posts = await _context.Posts.ToListAsync();
            return posts.AsQueryable();
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public void Update(Post item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
