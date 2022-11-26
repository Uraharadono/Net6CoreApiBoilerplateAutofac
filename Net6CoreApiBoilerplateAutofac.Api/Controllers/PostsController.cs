﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net6CoreApiBoilerplateAutofac.Api.Infrastructure;
using Net6CoreApiBoilerplateAutofac.Api.Models;
using Net6CoreApiBoilerplateAutofac.Api.Utility.Extensions;
using Net6CoreApiBoilerplateAutofac.Services.Post;
using Net6CoreApiBoilerplateAutofac.Services.Post.Dto;

namespace Net6CoreApiBoilerplateAutofac.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : BaseController
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet, Route("GetPosts")]
        public async Task<IActionResult> GetPosts()
        {
            var blogs = await _postService.GetPosts();
            return blogs != null ? Ok(blogs) : StatusCode(500);
        }

        [HttpGet, Route("GetPost")]
        public async Task<IActionResult> GetPost(long blogId)
        {
            var blog = await _postService.GetPost(blogId);
            return blog != null ? Ok(blog) : StatusCode(500);
        }

        [HttpPost, Route("AddPost")]
        public async Task<IActionResult> AddPost(PostViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelStateExtensions.GetErrorMessage(ModelState));

            PostDto dto = new PostDto
            {
                Id = model.Id,
                Title = model.Title,
                Content = model.Content,
                BlogId = model.BlogId
            };

            var addStatus = await _postService.AddPost(dto);
            return addStatus != null ? Ok() : StatusCode(500);
        }

        [HttpPut, Route("UpdatePost")]
        public async Task<IActionResult> UpdatePost(PostViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelStateExtensions.GetErrorMessage(ModelState));

            PostDto dto = new PostDto
            {
                Id = model.Id,
                Title = model.Title,
                Content = model.Content,
                BlogId = model.BlogId
            };

            var addStatus = await _postService.UpdatePost(dto);
            return addStatus ? Ok() : StatusCode(500);
        }
    }
}
