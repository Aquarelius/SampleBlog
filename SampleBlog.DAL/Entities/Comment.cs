using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SB.DAL.Entities
{
    [Table("Comments")]
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Added { get; set; }

        public string Text { get; set; }

        public int? ParentCommentId { get; set; }

        public virtual BlogPost Post { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}
