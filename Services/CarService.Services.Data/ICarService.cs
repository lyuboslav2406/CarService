namespace CarService.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ICarService
    {
        public Task<string> CreateAsync(int year, int modelId, int makeId, int fuelType, int transsmisionType, int cubicCapacity, string registrationNumber, int horsePower, string userId);

        Task<IEnumerable<string>> UploadAsync(Cloudinary cloudinary, ICollection<IFormFile> files);

        Task<string> AddImageInBase(IEnumerable<string> images, string carId);

        IEnumerable<T> GetAllByUserId<T>(string userId);

        public List<SelectListItem> GetFuelTypes();

        public List<SelectListItem> GetTransmission();
    }
}