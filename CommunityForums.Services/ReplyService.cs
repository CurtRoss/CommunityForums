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
    public class ReplyService
    {
        private readonly Guid _userId;
        public ReplyService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateReply(ReplyCreate model)
        {
            var entity =
                new Reply()
                {
                    OwnerId = _userId,
                    UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                    Content = model.Content,
                    CreateUtc = DateTimeOffset.Now
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Replies.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ReplyListItem> GetReplies()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Replies
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new ReplyListItem
                                {
                                    ReplyId = e.ReplyId,
                                    UserName = e.UserName,
                                    CreateUtc = e.CreateUtc
                                }
                            );
                return query.ToArray();
            }
        }

        public ReplyDetail GetReplyById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Replies
                        .Single(e => e.ReplyId == id && e.OwnerId == _userId);
                return
                    new ReplyDetail
                    {
                        ReplyId = entity.ReplyId,
                        Content = entity.Content,
                        CreatedUtc = entity.CreateUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateReply(ReplyEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Replies
                        .Single(e => e.ReplyId == model.ReplyId && e.OwnerId == _userId);

                entity.Content = model.Content;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteReply(int replyId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Replies
                        .Single(e => e.ReplyId == replyId && e.OwnerId == _userId);

                ctx.Replies.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
