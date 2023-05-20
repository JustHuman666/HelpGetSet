using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DAL.Enteties
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
