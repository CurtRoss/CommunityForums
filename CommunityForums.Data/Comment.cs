using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityForums.Data
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public Guid OwnerId { get; set; }
        public virtual List<Reply> ListOfReplies { get; set; } = new List<Reply>();

        [Required]
        public DateTimeOffset CreateUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
