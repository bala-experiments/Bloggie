using Bloggie.Web.Models.DomainModels;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogpostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogpostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogpostRepository = blogpostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagRepository.GetAllAsync();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.ID.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest blogpost)
        {
            //Assigning form data from view model to domain model
            //start
            var BlogPostModel = new BlogPost
            {
                Heading = blogpost.Heading,
                PageTitle = blogpost.PageTitle,
                Content = blogpost.Content,
                ShortDescription = blogpost.ShortDescription,
                FeaturedImageUrl = blogpost.FeaturedImageUrl,
                UrlHandle = blogpost.UrlHandle,
                PublishedDate = blogpost.PublishedDate,
                Author = blogpost.Author,
                Visible = blogpost.Visible
            };

            //adding the selected tags

            var selectedTags = new List<Tag>();

            foreach(var selectedTagId in blogpost.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagRepository.GetSync(selectedTagIdAsGuid);

                if(existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }

            //Mapping the selected tags to domain model
            BlogPostModel.Tags = selectedTags;
            //Ends

            //Invoke repository to save data in db
            await blogpostRepository.AddAsync(BlogPostModel);           

            return RedirectToAction("List");
            
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var model = await blogpostRepository.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid ID)
        {
            //Getting blog post data by ID
            var blogpost = await blogpostRepository.GetSync(ID);

            if(blogpost != null)
            {
                //to populate dropdown
                var tags = await tagRepository.GetAllAsync();
               
                //Geting 
                var blogpostmodel = new EditBlogPostRequest
                {
                    ID = blogpost.ID,
                    Heading = blogpost.Heading,
                    PageTitle = blogpost.PageTitle,
                    Content = blogpost.Content,
                    ShortDescription = blogpost.ShortDescription,
                    FeaturedImageUrl = blogpost.FeaturedImageUrl,
                    UrlHandle = blogpost.UrlHandle,
                    PublishedDate = blogpost.PublishedDate,
                    Author = blogpost.Author,
                    Visible = blogpost.Visible,
                    Tags = tags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.ID.ToString() }),
                    SelectedTags = blogpost.Tags.Select(x => x.ID.ToString()).ToArray()
                };

                return View(blogpostmodel);
            }

            return View(null);  
 
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest blogpost)
        {
            //Assigning form data from view model to domain model
            //start
            var BlogPostModel = new BlogPost
            {
                ID = blogpost.ID,
                Heading = blogpost.Heading,
                PageTitle = blogpost.PageTitle,
                Content = blogpost.Content,
                ShortDescription = blogpost.ShortDescription,
                FeaturedImageUrl = blogpost.FeaturedImageUrl,
                UrlHandle = blogpost.UrlHandle,
                PublishedDate = blogpost.PublishedDate,
                Author = blogpost.Author,
                Visible = blogpost.Visible
            };

            //adding the selected tags

            var selectedTags = new List<Tag>();

            foreach (var selectedTagId in blogpost.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagRepository.GetSync(selectedTagIdAsGuid);

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }

            //Mapping the selected tags to domain model
            BlogPostModel.Tags = selectedTags;
            //Ends

            //Invoke repository to save data in db
            await blogpostRepository.UpdateAsync(BlogPostModel);

            return RedirectToAction("List");

        }


        public async Task<IActionResult> Delete(Guid ID)
        {
            await blogpostRepository.DeleteSync(ID);
            return RedirectToAction("List");
        }
    }
}
