using DAL.Context;
using DAL.Enteties;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    /// <summary>
    /// Class that represents a volunteer repository
    /// </summary>
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly SiteContext _context;

        /// <summary>
        /// Constructor for creating a repository with given db context
        /// </summary>
        /// <param name="context">The instance of db context for the repository</param>
        public VolunteerRepository(SiteContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Volunteer item)
        {
            await _context.Volunteers.AddAsync(item);
        }

        public void Delete(int id)
        {
            var volunteer = _context.Volunteers.Find(id);
            if (volunteer != null)
            {
                _context.Volunteers.Remove(volunteer);
            }
        }

        public async Task<IQueryable<Volunteer>> GetAllAsync()
        {
            var volunteers = await _context.Volunteers.ToListAsync();
            return volunteers.AsQueryable();
        }

        public async Task<IQueryable<Volunteer>> GetAllWithDetailsAsync()
        {
            var volunteers = await _context.Volunteers
                .Include(volunteer => volunteer.Users).ToListAsync();
            return volunteers.AsQueryable();
        }

        public async Task<Volunteer> GetByIdAsync(int id)
        {
            return await _context.Volunteers.FindAsync(id);
        }

        public async Task<Volunteer> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Volunteers
                .Include(volunteer => volunteer.Users)
                .FirstOrDefaultAsync(volunteer => volunteer.Id == id);
        }

        public void Update(Volunteer item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
