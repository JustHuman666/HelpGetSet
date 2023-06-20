using DAL.Context;
using DAL.Enteties;
using DAL.Interfaces;
using DAL.Interfaces.BaseInterfaces;
using Microsoft.EntityFrameworkCore;


namespace DAL.Repositories
{
    /// <summary>
    /// Class that represents a message repository
    /// </summary>
    public class MessageRepository : IMessageRepository
    {
        private readonly SiteContext _context;

        /// <summary>
        /// Constructor for creating a repository with given db context
        /// </summary>
        /// <param name="context">The instance of db context for the repository</param>
        public MessageRepository(SiteContext context)
        {
            _context = context;
        }
        public async Task<Message> CreateAsync(Message item)
        {
            await _context.Messages.AddAsync(item);
            return item;
        }

        public void Delete(int id)
        {
            var message = _context.Messages.Find(id);
            if (message != null)
            {
                _context.Messages.Remove(message);
            }
        }

        public async Task<IQueryable<Message>> GetAllAsync()
        {
            var messages = await _context.Messages.ToListAsync();
            return messages.AsQueryable();
        }

        public async Task<IQueryable<Message>> GetMessagesByChatAsync(int id)
        {
            var messages = await _context.Messages.Where(message => message.ChatId == id).ToListAsync();
            return messages.AsQueryable();
        }

        public async Task<Message> GetByIdAsync(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public void Update(Message item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
