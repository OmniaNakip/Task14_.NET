using Microsoft.AspNetCore.Http;

namespace Movie_Management_System.Services.Interfaces
{
    public interface IImageService
    {
        Task<string?> SaveImageAsync(IFormFile image);
        void DeleteImage(string? imagePath);
    }
}