using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.Data.Regiones
{
    public class AddRegionData
    {
        [Required]
        [MaxLength(3, ErrorMessage = "Code has to be a minimum of 3 characters")]
        [MinLength(3, ErrorMessage = "Code has to be a max of 3 characters")]
        public required string Code { get; set; }

        [Required]
        public required string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
