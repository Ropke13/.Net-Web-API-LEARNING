using NZWalksAPI.Models.Data.Difficulty;
using NZWalksAPI.Models.Data.Regiones;

namespace NZWalksAPI.Models.Data.Walks
{
    public class WalkData
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public double LenghtInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        public required RegionData Region { get; set; }
        public required DifficultyData Difficulty { get; set; }
    }
}
