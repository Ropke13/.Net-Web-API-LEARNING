using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalksAPI.Models.Data.Image
{
    public class ImageUploadData
    {
        [Required]
        public required IFormFile File { get; set; }
        [Required]
        public required string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}
