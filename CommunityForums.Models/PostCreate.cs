using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityForums.Models
{
    public class PostCreate
    {

        [Required, MinLength(1, ErrorMessage = "Please enter at least one character.")]
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }

        

    }
}
