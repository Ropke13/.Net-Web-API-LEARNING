namespace NZWalksAPI.Models.DTO
{
    public class AddWalkData
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public double LenghtInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
