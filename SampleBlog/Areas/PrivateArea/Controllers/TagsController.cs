using System.Web.Http;
using SB.Areas.PrivateArea.Models;
using SB.BLL;
using SB.DAL.Repositories;

namespace SB.Areas.PrivateArea.Controllers
{
    public class TagsController : ApiController
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly TagsService _tagsService;

        public TagsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _tagsService = new TagsService(_unitOfWork);
        }

        [HttpGet]
        public string[] TagsAutocomplete(string text)
        {
            return _tagsService.GetAutocompleteSuggetions(text);
        }
    }
}
