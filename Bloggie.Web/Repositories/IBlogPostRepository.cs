using Bloggie.Web.Models.DomainModels;

namespace Bloggie.Web.Repositories
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetSync(Guid ID);
        Task<BlogPost> AddAsync(BlogPost blogpost);
        Task<BlogPost?> UpdateAsync(BlogPost blogpost);
        Task<BlogPost?> DeleteSync(Guid ID);
    }
}
