using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiSecurityDemo.Model.Db
{
    [Table("COMMENTS")]
    public class Comment
    {
        [Key]
        public long IdComment { get; set; }

        public string Name { get; set; }
        public string Content { get; set; }

        public long PostId { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
    }
}