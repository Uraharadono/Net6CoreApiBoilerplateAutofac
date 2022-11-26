﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6CoreApiBoilerplateAutofac.Infrastructure.DbUtility;
using Net6CoreApiBoilerplateAutofac.Infrastructure.Services;
using Net6CoreApiBoilerplateAutofac.Services.Post.Dto;
using NLog;
#pragma warning disable 1998

namespace Net6CoreApiBoilerplateAutofac.Services.Post
{
    public interface IPostService : IService
    {
        Task<List<PostDto>> GetPosts();
        Task<PostDto> GetPost(long postId);
        Task<PostDto> AddPost(PostDto dto);
        Task<bool> UpdatePost(PostDto dto);
    }

    public class PostService : IPostService
    {
        private readonly IUnitOfWork _uow;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public PostService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<PostDto>> GetPosts()
        {
            try
            {
                var dbPosts = _uow.Query<DbContext.Entities.Post>().ToList();

                if (!dbPosts.Any())
                    return null;

                return dbPosts.Select(s => new PostDto
                {
                    Id = s.Oid,
                    BlogId = s.BlogId,
                    Content = s.Content,
                    Title = s.Title
                }).ToList();
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return null;
            }
        }

        public async Task<PostDto> GetPost(long postId)
        {
            try
            {
                var dbPost = _uow.Query<DbContext.Entities.Post>(s => s.Oid == postId).FirstOrDefault();
                if (dbPost == null)
                    return null;

                return new PostDto
                {
                    Id = dbPost.Oid,
                    BlogId = dbPost.BlogId,
                    Content = dbPost.Content,
                    Title = dbPost.Title
                };
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return null;
            }
        }

        public async Task<PostDto> AddPost(PostDto dto)
        {
            try
            {
                var dbPost = new DbContext.Entities.Post
                {
                    BlogId = dto.BlogId,
                    Content = dto.Content,
                    Title = dto.Title
                };

                _uow.Context.Set<DbContext.Entities.Post>().Add(dbPost);
                _uow.Commit();

                dto.Id = dbPost.Oid;

                return dto;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return null;
            }
        }

        public async Task<bool> UpdatePost(PostDto dto)
        {
            try
            {
                var dbPost = _uow.Query<DbContext.Entities.Post>(s => s.Oid == dto.Id).FirstOrDefault();

                if (dbPost == null)
                    return false;

                dbPost.Oid = dto.Id;
                dbPost.BlogId = dto.BlogId;
                dbPost.Content = dto.Content;
                dbPost.Title = dto.Title;
                _uow.Commit();

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return false;
            }
        }
    }
}
