using Bloggie.Web.Data;
using Bloggie.Web.Models.DomainModels;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private ITagRepository _repository;
        public AdminTagsController(ITagRepository repository)
        {
            this._repository = repository;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {           
            var Tag = new Tag()
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };            
            await _repository.AddAsync(Tag);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tagslist = await _repository.GetAllAsync();
            return View(tagslist);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var tag = await _repository.GetSync(Id);
            if(tag != null)
            {
                var edittag = new EditTagRequest()
                {
                    ID = tag.ID,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(edittag);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTag)
        {
            var tag = new Tag
            {
                ID = editTag.ID,
                Name = editTag.Name,
                DisplayName = editTag.DisplayName
            };

            await _repository.UpdateAsync(tag);
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid ID)
        {
            await _repository.DeleteSync(ID);
            return RedirectToAction("List");

        }
    }
}
