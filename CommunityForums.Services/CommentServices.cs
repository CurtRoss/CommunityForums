using CommunityForums.Data;
using CommunityForums.Models;
using CommunityForums.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityForums.Services
{
    public class CommentServices
    {
        private readonly Guid _userId;

        public CommentServices(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateComment(CommentCreate model) 
        {
            var entity =
                 new Comment()
                 {
                     OwnerId = _userId,
                     Author = model.Author,
                     Text = model.Text,
                     PostId = model.PostId,
                     CreateUtc = DateTimeOffset.Now
                 };

            
            using (var ctx = new ApplicationDbContext())
            {
                var post = ctx.Posts.Find(entity.PostId);
                post.ListOfComments.Add(entity);
                ctx.Comments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<CommentListItem> GetComments() 
        {
            using (var ctx = new ApplicationDbContext()) 
            {
                var query =
                   ctx
                       .Comments
                       .Where(e => e.OwnerId == _userId)
                       .Select(
                           e =>
                               new CommentListItem
                               {
                                   Id = e.Id,
                                   Author = e.Author,
                                   CreatedUtc = e.CreateUtc,
                                   ModifiedUtc = e.ModifiedUtc,
                               }
                   );
                return query.ToArray();
            }
        }

        public CommentDetail GetCommentById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Comments
                        .Single(e => e.Id == id && e.OwnerId == _userId);
                return
                    new CommentDetail
                    {
                        Id = entity.Id,
                        Text = entity.Text,
                        CreateUtc = entity.CreateUtc,
                        ModifiedUtc = entity.ModifiedUtc,
                        ListOfReplies = entity.ListOfReplies
                    };
            }


        }

        public bool UpdateComment(CommentEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Comments
                        .Single(e => e.Id == model.Id && e.OwnerId == _userId);

                entity.Text = model.Text;
                entity.Id = model.Id;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteComment(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Comments
                        .Single(e => e.Id == Id && e.OwnerId == _userId);

                ctx.Comments.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
