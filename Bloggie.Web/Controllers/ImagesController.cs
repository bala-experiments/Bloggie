using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        public async Task<IActionResult> UploadSync(IFormFile file)
        {
            return Ok("This is the best image api call");
        }

    }
}
