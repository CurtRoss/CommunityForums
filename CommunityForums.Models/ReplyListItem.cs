using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityForums.Models
{
    public class ReplyListItem
    {
        public int ReplyId { get; set; }

        public string UserName { get; set; }


        [Display(Name ="Reply Date")]
        public DateTimeOffset CreateUtc { get; set; }

    }
}
