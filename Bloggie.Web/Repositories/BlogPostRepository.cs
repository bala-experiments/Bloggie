using Azure;
using Bloggie.Web.Data;
using Bloggie.Web.Models.DomainModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private BloggieDbContext _bloggieDbContext;
        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            this._bloggieDbContext = bloggieDbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogpost)
        {
            await _bloggieDbContext.BlogPosts.AddAsync(blogpost);
            await _bloggieDbContext.SaveChangesAsync();

            return blogpost;
        }

        public async Task<BlogPost?> DeleteSync(Guid ID)
        {
            var existingblogpost = await _bloggieDbContext.BlogPosts.FindAsync(ID);

            if(existingblogpost != null)
            {
                _bloggieDbContext.BlogPosts.Remove(existingblogpost);
                await _bloggieDbContext.SaveChangesAsync();
            }

            return existingblogpost;

        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {            
            return await _bloggieDbContext.BlogPosts.Include(x=>x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetSync(Guid ID)
        {
            return await _bloggieDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x => x.ID == ID);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogpost)
        {
            var existingtag = await _bloggieDbContext.BlogPosts.FirstOrDefaultAsync(x => x.ID == blogpost.ID); ;

            if (existingtag != null)
            {
                existingtag.Heading = blogpost.Heading;
                existingtag.PageTitle = blogpost.PageTitle;
                existingtag.Content = blogpost.Content;
                existingtag.ShortDescription = blogpost.ShortDescription;
                existingtag.FeaturedImageUrl = blogpost.FeaturedImageUrl;
                existingtag.UrlHandle = blogpost.UrlHandle;
                existingtag.PublishedDate = blogpost.PublishedDate;
                existingtag.Author = blogpost.Author;
                existingtag.Visible = blogpost.Visible;
                existingtag.Tags = blogpost.Tags;

                await _bloggieDbContext.SaveChangesAsync();

            }

            return existingtag;
        }
    }
}
