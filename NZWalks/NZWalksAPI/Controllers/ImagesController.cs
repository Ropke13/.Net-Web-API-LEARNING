using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Data.Image;
using NZWalksAPI.Models.Entity;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        public ImagesController(IImageRepository rep) 
        {
            this._imageRepository = rep;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadData data)
        {
            ValidateFileUpload(data);

            if (ModelState.IsValid)
            {
                var image = new Image
                {
                    File = data.File,
                    FileExtension = Path.GetExtension(data.FileName),
                    FileSizeInBytes = data.File.Length,
                    FileName = data.FileName,
                    FileDescription = data.FileDescription,
                };

                var result = await _imageRepository.Upload(image);

                return Ok(result);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadData data)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(data.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (data.File.Length > 10485760) 
            {
                ModelState.AddModelError("file", "File is too large! must be less then 10MB");
            }

        }
    }
}       
