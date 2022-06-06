using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiSecurityDemo.Model.Db
{
    [Table("POSTS")]
    public class Post
    {
        [Key]
        public long IdPost { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PulishDate { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}