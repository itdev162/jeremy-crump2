using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public PostsController(IMediator mediator, DataContext context)
        {
            this.mediator = mediator;
            this.context = context;
        } 
        
        public async Task<ActionResult<List<Post>>> List()
        {
            return await this.mediator.Send(new List.Query());
        }

        [HttpGet]
        public ActionResult<List<Post>> Get()
        {
            return this.context.Posts.ToList();
        }

        [HttpGet("{id}")]

        public ActionResult<Post> GetById(Guid id)
        {
            return this.context.Posts.Find(id);
        }

        public ActionResult <Post> Create([FromBody] Post request)
        {
            var post = new Post 
            {
                Id = request.Id,
                Title = request.Title,
                Body = request.Body,
                Date = request.Date
            };

            context.Posts.Add(post);
            var success = context.SaveChanges() > 0;

            if(success)
            {
                return post;
            }

            throw new Exception("Error creating post");
        }

    }

  
}