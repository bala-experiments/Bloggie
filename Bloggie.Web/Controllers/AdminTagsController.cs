using Bloggie.Web.Data;
using Bloggie.Web.Models.DomainModels;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private BloggieDbContext _bloggieDbContext;
        public AdminTagsController(BloggieDbContext bloggieDbContext)
        {
            this._bloggieDbContext = bloggieDbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddTagRequest addTagRequest)
        {           
            var Tag = new Tag()
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            _bloggieDbContext.Tags.Add(Tag);
            _bloggieDbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult List()
        {
            var tagslist = _bloggieDbContext.Tags.AsEnumerable<Tag>();
            return View(tagslist);
        }

        [HttpGet]
        public IActionResult Edit(Guid Id)
        {
            var tag = _bloggieDbContext.Tags.FirstOrDefault(x => x.ID == Id);
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
        public IActionResult Edit(EditTagRequest editTag)
        {
            var existingtag = _bloggieDbContext.Tags.Where(x => x.ID == editTag.ID).FirstOrDefault();

            if (existingtag != null)
            {
                existingtag.Name = editTag.Name;
                existingtag.DisplayName = editTag.DisplayName;
                _bloggieDbContext.SaveChanges();

                return RedirectToAction("List");
            }

            return View(null);
            
        }

        [HttpPost]
        public IActionResult Delete(Guid ID)
        {           
            var existingtag = _bloggieDbContext.Tags.Find(ID);

            if (existingtag != null)
            {
                _bloggieDbContext.Tags.Remove(existingtag);
                _bloggieDbContext.SaveChanges();               
            }

            return RedirectToAction("List");

        }
    }
}
