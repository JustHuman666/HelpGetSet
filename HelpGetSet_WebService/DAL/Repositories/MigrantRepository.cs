using DAL.Context;
using DAL.Enteties;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    /// <summary>
    /// Class that represents a migrant repository
    /// </summary>
    public class MigrantRepository : IMigrantRepository
    {
        private readonly SiteContext _context;

        /// <summary>
        /// Constructor for creating a repository with given db context
        /// </summary>
        /// <param name="context">The instance of db context for the repository</param>
        public MigrantRepository(SiteContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Migrant item)
        {
            await _context.Migrants.AddAsync(item);
        }

        public void Delete(int id)
        {
            var migrant = _context.Migrants.Find(id);
            if (migrant != null)
            {
                _context.Migrants.Remove(migrant);
            }
        }

        public async Task<IQueryable<Migrant>> GetAllAsync()
        {
            var migrants = await _context.Migrants.ToListAsync();
            return migrants.AsQueryable();
        }

        public async Task<IQueryable<Migrant>> GetAllWithDetailsAsync()
        {
            var migrants = await _context.Migrants
                .Include(migrant => migrant.User).ToListAsync();
            return migrants.AsQueryable();
        }

        public async Task<Migrant> GetByIdAsync(int id)
        {
            return await _context.Migrants.FindAsync(id);
        }

        public async Task<Migrant> GetByUserIdAsync(int id)
        {
            return await _context.Migrants.Where(migrant => migrant.UserId == id).FirstOrDefaultAsync();
        }

        public async Task<Migrant> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Migrants
                .Include(migrant => migrant.User)
                .FirstOrDefaultAsync(migrant => migrant.Id == id);
        }

        public void Update(Migrant item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
