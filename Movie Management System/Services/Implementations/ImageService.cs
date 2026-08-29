using Microsoft.AspNetCore.Http;
using Movie_Management_System.Services.Interfaces;

namespace Movie_Management_System.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;

        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string?> SaveImageAsync(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return null;

            var uploadsFolder =
                Path.Combine(
                    _environment.WebRootPath,
                    "images",
                    "movies");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName =
                Guid.NewGuid().ToString() +
                Path.GetExtension(image.FileName);

            var filePath =
                Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(
                filePath,
                FileMode.Create);

            await image.CopyToAsync(stream);

            return "/images/movies/" + fileName;
        }

        public void DeleteImage(string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return;

            var filePath = Path.Combine(
                _environment.WebRootPath,
                imagePath.TrimStart('/'));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}