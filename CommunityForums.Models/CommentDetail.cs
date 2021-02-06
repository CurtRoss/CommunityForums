using CommunityForums.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityForums.Models
{
   public class CommentDetail
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<Reply> ListOfReplies { get; set; } = new List<Reply>();


        public DateTimeOffset CreateUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
