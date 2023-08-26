using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.Data.Walks
{
    public class AddWalkData
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        [Range(0, 50)]
        public double LenghtInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}
