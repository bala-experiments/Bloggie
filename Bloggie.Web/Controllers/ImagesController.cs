using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bloggie.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        public async Task<IActionResult> UploadSync(IFormFile file)
        {
           var imageurl = await imageRepository.UploadAsync(file);  

            if (imageurl == null)
            {
                return Problem("Something went wrong!!",null,(int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(new {Link = imageurl});
        }

    }
}
