using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityForums.Data
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public DateTimeOffset CreateUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
