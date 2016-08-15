using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SB.Areas.PrivateArea.Models
{
    public class EditPostViewModel
    {

        public EditPostViewModel()
        {
            Tags = "[]";
        }
        [Required]
        public int Id { get; set; }

        [DisplayName("Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "How we'll call it?")]
        public string Title { get; set; }

        [DisplayName("Body")]
        [AllowHtml]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, write something")]
        public string Text { get; set; }

        [DisplayName("Tags")]
        public string Tags { get; set; }
    }
}