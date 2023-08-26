using NZWalksAPI.Models.Entity;

namespace NZWalksAPI.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
