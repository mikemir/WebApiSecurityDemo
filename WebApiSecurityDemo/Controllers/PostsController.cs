using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSecurityDemo.Model.Db;
using WebApiSecurityDemo.Services;
using WebApiSecurityDemo.Utils.Middlewares;

namespace WebApiSecurityDemo.Controllers
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: api/Posts
        [HttpGet]
        [LimitRequests(MaxRequests = 5, TimeWindow = 10)]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts([FromQuery] string searchTitle)
        {
            var posts = await _postService.GetPosts(searchTitle); ;

            return Ok(posts);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        [LimitRequests(MaxRequests = 2, TimeWindow = 5)]
        public async Task<ActionResult<Post>> GetPost(long id)
        {
            var post = await _postService.GetPostById(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                post.IdPost,
                post.Title,
                post.Content
            });
        }

        //// POST: api/Posts
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Post>> PostPost(Post post)
        //{
        //    _context.Posts.Add(post);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetPost", new { id = post.IdPost }, post);
        //}
    }
}