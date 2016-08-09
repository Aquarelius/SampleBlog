using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.DAL.Entities
{
    [Table("Tags")]
    public class Tag
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(127)]
        public string Label { get; set; }

        public virtual ICollection<BlogPost> Posts { get; set; } 
    }
}
