using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSecurityDemo.Model.Db;

namespace WebApiSecurityDemo.Services
{
    public class PostService : IPostService
    {
        private readonly BlogDbContext _dbContext;

        public PostService(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Post> GetPostById(long id)
        {
            var post = await _dbContext.Posts
                                        .FromSqlRaw($"EXEC sp_getpost {id}")
                                        .ToListAsync();

            return post.FirstOrDefault();
        }

        public async Task<List<Post>> GetPosts(string textSearch)
        {
            //https://docs.microsoft.com/es-es/ef/core/querying/raw-sql
            //var param = new SqlParameter("textSearch", $"%{textSearch}%");
            var posts = await _dbContext.Posts
                                        .FromSqlRaw($"SELECT * FROM POSTS WHERE Title LIKE '%{textSearch}%'")
                                        .ToListAsync();

            return posts;
        }
    }
}