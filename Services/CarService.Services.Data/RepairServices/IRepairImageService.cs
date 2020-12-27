namespace CarService.Services.Data.RepairServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using global::CarService.Data.Models.CarRepair;
    using Microsoft.AspNetCore.Http;

    public interface IRepairImageService
    {
        string GetImageUrlByRepairId(string repairId);

        List<RepairImage> GetImagesUrlsByRepairId(string repairId);

        Task<IEnumerable<string>> UploadAsync(Cloudinary cloudinary, ICollection<IFormFile> files);

        Task<string> AddImageInBase(IEnumerable<string> images, string repairId);
    }
}
