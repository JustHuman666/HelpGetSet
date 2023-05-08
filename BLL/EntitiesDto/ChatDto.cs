using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.EntitiesDto
{
    /// <summary>
    /// Class that represents a chat between two or more users
    /// </summary>
    public class ChatDto
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string ChatName { get; set; }
        public virtual ICollection<int> UserIds { get; set; }
        public virtual ICollection<int> MessageIds { get; set; }

    }
}
