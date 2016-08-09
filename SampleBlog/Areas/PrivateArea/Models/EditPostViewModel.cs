using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SB.Areas.PrivateArea.Models
{
    public class EditPostViewModel
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "How we'll call it?")]
        public string Title { get; set; }

        [DisplayName("Body")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, write something")]
        public string Text { get; set; }
    }
}