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
    public class PostServices
    {
        private readonly Guid _userId;

        public PostServices(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePost(PostCreate model)
        {
            var entity =
                new Post()
                {
                    OwnerId = _userId,
                    Title = model.Title,
                    Content = model.Content,
                    UserName = model.UserName,
                    CreateUtc = DateTimeOffset.Now
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Posts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PostListItem> GetPosts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Posts
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new PostListItem
                                {
                                    PostId = e.PostId,
                                    UserName = e.UserName,
                                    Title = e.Title,
                                    CreatedUtc = e.CreateUtc,
                                    ModifiedUtc = e.ModifiedUtc,
                                }
                    );
                return query.ToArray();
            }
        }

        public PostDetail GetPostByid(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Posts
                        .Single(e => e.PostId == id && e.OwnerId == _userId);
                return
                    new PostDetail
                    {
                        PostId = entity.PostId,
                        Title = entity.Title,
                        Content = entity.Content,
                        CreatedUtc = entity.CreateUtc,
                        ModifiedUtc = entity.ModifiedUtc,
                        ListOfComments = entity.ListOfComments
                    };
            }
        }

        public bool UpdatePost(PostEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Posts
                        .Single(e => e.PostId == model.PostId && e.OwnerId == _userId);

                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePost(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Posts
                        .Single(e => e.PostId == id && e.OwnerId == _userId);

                ctx.Posts.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
