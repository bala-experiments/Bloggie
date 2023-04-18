using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Bloggie.Web.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration configuration;
        private readonly Cloudinary cloudinary;

        //Initializing cloudinary account
        //refer code for documentation in cliudinary
        public CloudinaryImageRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            cloudinary = new Cloudinary(new Account(configuration.GetSection("Cloudinary")["CloudName"], configuration.GetSection("Cloudinary")["APIKey"], configuration.GetSection("Cloudinary")["APISecret"]));
        }

        //Uploading the file to imaginary cloud
        public async Task<string> UploadAsync(IFormFile file)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName,file.OpenReadStream()),
                DisplayName = file.FileName
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);           

            if (uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK){

                return uploadResult.SecureUrl.ToString();
            }


            return null;
        }
    }
}
