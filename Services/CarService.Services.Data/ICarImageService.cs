namespace CarService.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using global::CarService.Data.Models.CarElements;
    using Microsoft.AspNetCore.Http;

    public interface ICarImageService
    {
        string GetImageUrlByCarId(string carId);

        List<CarImage> GetImagesUrlsByCarId(string carId);

        Task<IEnumerable<string>> UploadAsync(Cloudinary cloudinary, ICollection<IFormFile> files);

        Task<string> AddImageInBase(IEnumerable<string> images, string carId);
    }
}
