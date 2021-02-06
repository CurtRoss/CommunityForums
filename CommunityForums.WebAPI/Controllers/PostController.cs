using CommunityForums.Models;
using CommunityForums.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommunityForums.WebAPI.Controllers
{
    [Authorize]
    public class PostController : ApiController
    {
        private PostServices CreatePostService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var postService = new PostServices(userId);
            return postService;
        }

        public IHttpActionResult Get()
        {
            PostServices postService = CreatePostService();
            var posts = postService.GetPosts();
            return Ok(posts);
        }

        public IHttpActionResult Post(PostCreate post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            PostServices service = CreatePostService();

            if (!service.CreatePost(post))
                return InternalServerError();

            return Ok();
        }

        //Get post by post id
        public IHttpActionResult Get(int id)
        {
            PostServices postService = CreatePostService();
            var post = postService.GetPostByid(id);
            return Ok(post);
        }

        public IHttpActionResult Put(PostEdit post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePostService();

            if (!service.UpdatePost(post))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreatePostService();

            if (!service.DeletePost(id))
                return InternalServerError();

            return Ok();
        }
    }
}
