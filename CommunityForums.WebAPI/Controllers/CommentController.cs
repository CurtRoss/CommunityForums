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
    public class CommentController : ApiController
    {
        private CommentServices CreateCommentServices()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var commentServices = new CommentServices(userId);
            return commentServices;
        }

        public IHttpActionResult Get()
        {
            CommentServices noteService = CreateCommentServices();
            var notes = noteService.GetComments();
            return Ok(notes);
        }

        public IHttpActionResult Post(CommentCreate comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCommentServices();

            if (!service.CreateComment(comment))
                return InternalServerError();

            return Ok();
        }



        public IHttpActionResult Get(int id)
        {
            CommentServices commentServices = CreateCommentServices();
            var comment = commentServices.GetCommentById(id);
            return Ok(comment);
        }

        public IHttpActionResult Put(CommentEdit comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCommentServices();

            if (!service.UpdateComment(comment))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateCommentServices();

            if (!service.DeleteComment(id))
                return InternalServerError();

            return Ok();
        }
    }
}
