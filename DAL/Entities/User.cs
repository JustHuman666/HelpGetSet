using Microsoft.AspNetCore.Identity;

namespace DAL.Enteties
{
    public class User : IdentityUser<int>
    {
        public virtual UserProfile? UserProfile { get; set; }
    }
}
