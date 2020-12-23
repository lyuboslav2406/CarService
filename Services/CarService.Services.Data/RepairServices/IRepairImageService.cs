using CarService.Data.Models.CarRepair;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Services.Data.RepairServices
{
    public interface IRepairImageService
    {
        string GetImageUrlByRepairId(string repairId);

        List<RepairImage> GetImagesUrlsByRepairId(string repairId);

        Task<IEnumerable<string>> UploadAsync(Cloudinary cloudinary, ICollection<IFormFile> files);

        Task<string> AddImageInBase(IEnumerable<string> images, string repairId);
    }
}
