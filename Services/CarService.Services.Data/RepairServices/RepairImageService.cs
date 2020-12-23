using CarService.Data.Common.Repositories;
using CarService.Data.Models.CarElements;
using CarService.Data.Models.CarRepair;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Services.Data.RepairServices
{
    public class RepairImageService : IRepairImageService
    {
        private readonly IDeletableEntityRepository<RepairImage> repairImageRepo;

        public RepairImageService(IDeletableEntityRepository<RepairImage> repairImageRepo)
        {
            this.repairImageRepo = repairImageRepo;
        }

        public async Task<string> AddImageInBase(IEnumerable<string> images, string repairId)
        {
            foreach (var image in images)
            {
                var imageUrl = new RepairImage
                {
                    Url = image,
                    RepairId = repairId,
                };
                await this.repairImageRepo.AddAsync(imageUrl);
                await this.repairImageRepo.SaveChangesAsync();
            }

            return repairId;
        }

        public List<RepairImage> GetImagesUrlsByRepairId(string repairId)
        {
            var images = this.repairImageRepo.All().Where(x => x.RepairId == repairId).ToList();

            return images;
        }

        public string GetImageUrlByRepairId(string repairId)
        {
            if (this.repairImageRepo.All().Where(x => x.RepairId == repairId).Any())
            {
                var image = this.repairImageRepo.All().Where(x => x.RepairId == repairId).FirstOrDefault().Url;
                return image;
            }

            return "data:image/jpeg;base64,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";
        }

        [Obsolete]
        public async Task<IEnumerable<string>> UploadAsync(Cloudinary cloudinary, ICollection<IFormFile> files)
        {
            List<string> imagesUrl = new List<string>();

            foreach (var file in files)
            {
                byte[] destinationImage;

                using (var image = new MemoryStream())
                {
                    await file.CopyToAsync(image);

                    destinationImage = image.ToArray();
                }

                using (var destinationStrem = new MemoryStream(destinationImage))
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, destinationStrem),
                    };

                    var result = await cloudinary.UploadAsync(uploadParams);

                    if (result.Error == null)
                    {
                        var imgUrl = result.Uri.AbsoluteUri;

                        imagesUrl.Add(imgUrl);
                    }
                }
            }

            return imagesUrl;
        }
    }
}
