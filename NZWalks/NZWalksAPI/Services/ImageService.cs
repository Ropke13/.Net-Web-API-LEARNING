using NZWalksAPI.Data;
using NZWalksAPI.Models.Entity;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Services
{
    public class ImageService : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnviroment;
        private readonly IHttpContextAccessor h;
        private readonly NZWalksDbContext context;
        public ImageService(IWebHostEnvironment webHostEnviroment, IHttpContextAccessor h, NZWalksDbContext context)
        {
            this.webHostEnviroment = webHostEnviroment;
            this.h = h;
            this.context = context;
        }

        public async Task<Image> Upload(Image image)
        {
            var localPath = Path.Combine(webHostEnviroment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            using var stream = new FileStream(localPath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{h.HttpContext.Request.Scheme}://{h.HttpContext.Request.Host}{h.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            
            image.FilePath = urlFilePath;

            await context.Images.AddAsync(image);
            await context.SaveChangesAsync();

            return image;
        }
    }
}
