using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityForums.Data
{
    public class Reply
    {
        [Key]
        public int ReplyId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        public string UserName { get; set; }

        public string Content { get; set; }

        [Required]
        public DateTimeOffset CreateUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
