﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net6CoreApiBoilerplateAutofac.Api.Infrastructure;
using Net6CoreApiBoilerplateAutofac.Api.Models;
using Net6CoreApiBoilerplateAutofac.Api.Utility.Extensions;
using Net6CoreApiBoilerplateAutofac.Services.Blog;
using Net6CoreApiBoilerplateAutofac.Services.Blog.Dto;

namespace Net6CoreApiBoilerplateAutofac.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : BaseController
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet, Route("GetBlogs")]
        public async Task<IActionResult> GetBlogs()
        {
            var blogs = await _blogService.GetBlogs();
            return blogs != null ? Ok(blogs) : StatusCode(500);
        }

        [HttpGet, Route("GetBlog")]
        public async Task<IActionResult> GetBlog(long blogId)
        {
            var blog = await _blogService.GetBlog(blogId);
            return blog != null ? Ok(blog) : StatusCode(500);
        }

        [HttpPost, Route("AddBlog")]
        public async Task<IActionResult> AddBlog(BlogViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelStateExtensions.GetErrorMessage(ModelState));

            BlogDto dto = new BlogDto
            {
                Id = model.Id,
                Url = model.Url
            };

            var addStatus = await _blogService.AddBlog(dto);
            return addStatus != null ? Ok() : StatusCode(500);
        }

        [HttpPut, Route("UpdateBlog")]
        public async Task<IActionResult> UpdateBlog(BlogViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelStateExtensions.GetErrorMessage(ModelState));

            BlogDto dto = new BlogDto
            {
                Id = model.Id,
                Url = model.Url
            };

            var addStatus = await _blogService.UpdateBlog(dto);
            return addStatus ? Ok() : StatusCode(500);
        }
    }
}
