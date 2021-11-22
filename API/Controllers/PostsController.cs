using System;
using System.Collections.Generic;
using System.linq;
using System.Threading.Tasks;
using Application.Posts;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PostsController : ControllerBase
    {
        private readonly IMediator mediator;

        private readonly DataContext context;
        
        public PostsController(IMediator mediator) => this.mediator = mediator;
        
        /// <summary>
        ///  GET api/posts
        /// </summary>
        /// <returns>A List of posts</returns>
        [HttpGet]
        public ActionResult<list<post>> Get()
        {
            return this.context.Posts.ToList();
        }

        /// <summary>
        /// GET api/post/[id]
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>A single post</returns>
        [HttpGet("{id}")]

        public ActionResult<Post> GetById(Guid id)
        {
            return this.context.Posts.Find(id);
        }

        public async Task<ActionResult<List<Post>>> List()
        {
            return await this.mediator.Send(new List.Query());
        }

    }

  
}