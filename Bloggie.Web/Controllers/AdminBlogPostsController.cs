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
                Tags = tags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Name })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest blogpost)
        {
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

            await blogpostRepository.AddAsync(BlogPostModel);

            //to be modified later.
            var tags = await tagRepository.GetAllAsync();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Name })
            };

            return View(model);
            
        }
    }
}
