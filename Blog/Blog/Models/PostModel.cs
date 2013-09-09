using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTimeOffset Date { get; set; }

        public virtual UserProfile Author { get; set; }
    }
}