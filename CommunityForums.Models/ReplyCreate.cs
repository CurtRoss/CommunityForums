using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityForums.Models
{
    public class ReplyCreate
    {
        [Required]
        [MaxLength(500)]
        public string Content { get; set; }
        public string UserName { get; set; }
        public int CommentId { get; set; }
    }
}
