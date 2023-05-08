using DAL.Enteties;
using DAL.Interfaces.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// Repository interface for working with chat
    /// </summary>
    public interface IChatRepository : IRepository<Chat>, IDetailsRepository<Chat>
    {
    }
}
