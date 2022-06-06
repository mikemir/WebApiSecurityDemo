using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiSecurityDemo.Model.Db;

namespace WebApiSecurityDemo.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetPosts(string textSearch);

        Task<Post> GetPostById(long id);
    }
}