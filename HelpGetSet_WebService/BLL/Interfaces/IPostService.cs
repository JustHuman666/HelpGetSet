using BLL.EntitiesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPostService
    {
        Task<PostDto> GetPostByIdAsync(int id);

        Task<IEnumerable<PostDto>> GetAllPostsAsync();

        Task<IEnumerable<PostDto>> GetMigrantsPostsAsync();

        Task<IEnumerable<PostDto>> GetVolunteersPostsAsync();

        Task<IEnumerable<PostDto>> GetAllUserPostsByUserIdAsync(int id);

        Task<IEnumerable<PostDto>> GetAllCountryPostsByCountryIdAsync(int id);

        Task CreatePostAsync(PostDto item);

        Task UpdatePostAsync(PostDto item);

        Task DeletePostAsync(int id);
    }
}
