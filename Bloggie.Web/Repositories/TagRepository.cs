using Bloggie.Web.Data;
using Bloggie.Web.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private BloggieDbContext _bloggieDbContext;
        public TagRepository(BloggieDbContext bloggieDbContext)
        {
            this._bloggieDbContext = bloggieDbContext;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await _bloggieDbContext.Tags.AddAsync(tag);
            await _bloggieDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteSync(Guid ID)
        {
            var existingtag = await _bloggieDbContext.Tags.FindAsync(ID);

            if (existingtag != null)
            {
                _bloggieDbContext.Tags.Remove(existingtag);
                await _bloggieDbContext.SaveChangesAsync();
            }

            return existingtag;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _bloggieDbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetSync(Guid ID)
        {
            return await _bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.ID == ID);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingtag = await _bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.ID == tag.ID); ;

            if (existingtag != null)
            {
                existingtag.Name = tag.Name;
                existingtag.DisplayName = tag.DisplayName;
                await _bloggieDbContext.SaveChangesAsync();                
            }

            return existingtag;
        }
    }
}
