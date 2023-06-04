using AutoMapper;
using BLL.EntitiesDto;
using BLL.Interfaces;
using BLL.Validation;
using DAL.Enteties;
using DAL.Interfaces.BaseInterfaces;
namespace BLL.Services
{
    /// <summary>
    /// Service for working with chats, message and users in them
    /// </summary>
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _db;

        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for creating of service with given unit of work (database and operations) and automapper profile
        /// </summary>
        /// <param name="database">Instance of unit of work for this service</param>
        /// <param name="mapper">Instance of automapper profile</param>
        public ChatService(IUnitOfWork database, IMapper mapper)
        {
            _db = database;
            _mapper = mapper;
        }

        public async Task AddUserInChatAsync(int chatId, int userId)
        {
            var chat = await _db.Chats.GetByIdAsync(chatId);
            if (chat == null)
            {
                throw new NotFoundException("Chat does not exist");
            }
            var user = await _db.Users.GetByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException("User does not exist");
            }
            if (chat.Users.Any(chat => chat.UserId == userId))
            {
                throw new HelpSiteException("This user is already in this chat");
            }
            chat.Users.Add(new UserChat() { UserId = userId, ChatId = chatId, isAdmin = false });
            _db.Chats.Update(chat);
            await _db.SaveAsync();
        }

        public async Task ClearChatHistoryByIdAsync(int id, int userId)
        {
            var chat = await _db.Chats.GetByIdWithDetailsAsync(id);
            if (chat == null)
            {
                throw new NotFoundException("Chat does not exist");
            }
            if (chat.Messages.Count() == 0 || chat.Messages == null)
            {
                throw new NotFoundException("Chat does not have any message");
            }
            var user = chat.Users.FirstOrDefault(x => x.UserId == userId);
            if (user == null)
            {
                throw new NotFoundException("This user does not in this chat");
            }
            if (!user.isAdmin)
            {
                throw new HelpSiteException("Only admin of chat can clear history of chat");
            }
            var messages = await _db.Messages.GetAllAsync();
            var chatMessages = messages.Where(message => message.ChatId == id);
            foreach (var message in chatMessages)
            {
                _db.Messages.Delete(message.Id);
            }
            await _db.SaveAsync();
        }

        public async Task CreateChatAsync(ChatDto item)
        {
            if (item == null)
            {
                throw new HelpSiteException("Chat cannot be null");
            }
            if (item.UserIds.Count() < 1)
            {
                throw new HelpSiteException("Chat should consist of at least one user");
            }
            if (string.IsNullOrEmpty(item.ChatName))
            {
                throw new HelpSiteException("Chat name cannot be null or empty");
            }
            if (item.UserIds.Count == 1)
            {
                var user = await _db.UsersProfiles.GetByIdWithDetailsAsync(item.UserIds.First());
                if (user.Chats != null && user.Chats.Count() != 0)
                {
                    var userChats = await GetAllChatsByUserIdAsync(item.UserIds.First());
                    if (userChats != null && userChats.Any(chat => chat.UserIds.Count == 1))
                    {
                        throw new HelpSiteException("Cannot create more than one chat with one user for this user");
                    }
                }

            }
            item.CreationDate = DateTime.Now;
            item.MessageIds = new HashSet<int>();

            var chatToCreate = _mapper.Map<Chat>(item);
            chatToCreate.Users.First().isAdmin = true;
            await _db.Chats.CreateAsync(chatToCreate);
            await _db.SaveAsync();
        }

        public async Task DeleteChatAsync(int id, int userId)
        {
            var chat = await _db.Chats.GetByIdWithDetailsAsync(id);
            if (chat == null)
            {
                throw new NotFoundException($"Chat with id: {id} does not exist");
            }
            var user = chat.Users.FirstOrDefault(x => x.UserId == userId);
            if (user == null)
            {
                throw new NotFoundException($"The user with id: {userId} does not exist in this chat");
            }
            if (!user.isAdmin)
            {
                throw new HelpSiteException("Only admin of chat can delete chat");
            }
            _db.Chats.Delete(id);
            await _db.SaveAsync();
        }

        public async Task DeleteUserFromChatAsync(int chatId, int userId, int whoDelete)
        {
            var chat = await _db.Chats.GetByIdWithDetailsAsync(chatId);
            if (chat == null)
            {
                throw new NotFoundException("Chat does not exist");
            }
            var userInChat = chat.Users.FirstOrDefault(user => user.UserId == userId);
            if (userInChat == null)
            {
                throw new NotFoundException("This user is not in that chat");
            }
            if (userId != whoDelete && !chat.Users.FirstOrDefault(x => x.UserId == whoDelete).isAdmin)
            {
                throw new HelpSiteException("Only admin of chat or actually this user can delete him from chat");
            }

            chat.Users.Remove(userInChat);
            _db.Chats.Update(chat);

            if (chat.Users.Count() == 0)
            {
                _db.Chats.Delete(chatId);
            }
            await _db.SaveAsync();
        }

        public async Task<IEnumerable<ChatDto>> GetAllChatsAsync()
        {
            var chats = await _db.Chats.GetAllAsync();
            if (chats == null || chats.Count() == 0)
            {
                throw new NotFoundException("Any chat does not exist");
            }
            return _mapper.Map<IEnumerable<ChatDto>>(chats);
        }

        public async Task<IEnumerable<ChatDto>> GetAllChatsByUserIdAsync(int id)
        {
            var user = await _db.Users.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User does not exist");
            }
            var chats = await _db.Chats.GetAllWithDetailsAsync();
            if (chats == null || chats.Count() == 0)
            {
                throw new NotFoundException("Any chat does not exist");
            }
            var chatsDto = _mapper.Map<IEnumerable<ChatDto>>(chats);
            var userChats = chatsDto.Where(chat => chat.UserIds.Contains(id));
            if (userChats == null || userChats.Count() == 0)
            {
                throw new NotFoundException("This user does not have any chat");
            }
            return userChats;
        }

        public async Task<IEnumerable<UserProfileDto>> GetAllUsersOfChatAsync(int id)
        {
            var chat = await _db.Chats.GetByIdWithDetailsAsync(id);
            if (chat == null)
            {
                throw new NotFoundException("Chat does not exist");
            }
            var users = new List<UserProfile>();
            foreach (var userInChat in chat.Users)
            {
                users.Add(await _db.UsersProfiles.GetByIdWithDetailsAsync(userInChat.UserId));
            }
            return _mapper.Map<IEnumerable<UserProfileDto>>(users);
        }

        public async Task<IEnumerable<ChatDto>> GetAllWithDetailsAsync()
        {
            var chats = await _db.Chats.GetAllWithDetailsAsync();
            if (chats == null || chats.Count() == 0)
            {
                throw new NotFoundException("Any chat does not exist");
            }
            return _mapper.Map<IEnumerable<ChatDto>>(chats);
        }

        public async Task<ChatDto> GetChatByIdAsync(int id)
        {
            var chat = await _db.Chats.GetByIdAsync(id);
            if (chat == null)
            {
                throw new NotFoundException("Chat does not exist");
            }
            return _mapper.Map<ChatDto>(chat);
        }

        public async Task<ChatDto> GetChatByIdWithDetailsAsync(int id)
        {
            var chat = await _db.Chats.GetByIdWithDetailsAsync(id);
            if (chat == null)
            {
                throw new NotFoundException("Chat does not exist");
            }
            return _mapper.Map<ChatDto>(chat);
        }

        public async Task RenameChatAsync(int chatId, string chatName)
        {
            var chat = await _db.Chats.GetByIdWithDetailsAsync(chatId);
            if (chat == null)
            {
                throw new NotFoundException("Chat does not exist");
            }
            if (string.IsNullOrEmpty(chatName))
            {
                throw new HelpSiteException("Chat name cannot be null or empty");
            }
            chat.ChatName = chatName;
            _db.Chats.Update(chat);
            await _db.SaveAsync();
        }

        public async Task SetAdminStatusToUserAsync(int userId, int adminId, int chatId)
        {
            var chat = await _db.Chats.GetByIdWithDetailsAsync(userId);
            if (chat == null)
            {
                throw new NotFoundException("Chat does not exist");
            }
            var userToChange = chat.Users.FirstOrDefault(x => x.UserId == userId); ;
            if (userToChange == null)
            {
                throw new NotFoundException("User does not exist in this chat");
            }
            if (userToChange.isAdmin)
            {
                throw new HelpSiteException("This user is already admin of this chat");
            }
            var chatAdmin = chat.Users.FirstOrDefault(x => x.UserId == adminId);
            if (chatAdmin == null)
            {
                throw new NotFoundException("User does not exist in this chat");
            }
            if (!chatAdmin.isAdmin)
            {
                throw new HelpSiteException("Only admin of chat can set admin status to another user");
            }
            userToChange.isAdmin = true;
            await _db.SaveAsync();
        }

        public async Task SetDefaultStatusToUserAsync(int userId, int adminId, int chatId)
        {
            var chat = await _db.Chats.GetByIdWithDetailsAsync(userId);
            if (chat == null)
            {
                throw new NotFoundException("Chat does not exist");
            }
            var userToChange = chat.Users.FirstOrDefault(x => x.UserId == userId); ;
            if (userToChange == null)
            {
                throw new NotFoundException("User does not exist in this chat");
            }
            if (!userToChange.isAdmin)
            {
                throw new HelpSiteException("This user already has default role");
            }
            var chatAdmin = chat.Users.FirstOrDefault(x => x.UserId == adminId);
            if (chatAdmin == null)
            {
                throw new NotFoundException("User does not exist in this chat");
            }
            if (!chatAdmin.isAdmin)
            {
                throw new HelpSiteException("Only admin of chat can set admin status to another user");
            }
            userToChange.isAdmin = false;
            await _db.SaveAsync();
        }
    }
}
