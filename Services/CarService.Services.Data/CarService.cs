namespace CarService.Services.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using global::CarService.Data.Common.Repositories;
    using global::CarService.Data.Models.CarElements;
    using global::CarService.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CarService : ICarService
    {
        private readonly IDeletableEntityRepository<Car> carRepository;
        private readonly IDeletableEntityRepository<CarImage> carImagesRepository;

        public CarService(
            IDeletableEntityRepository<Car> carRepository,
            IDeletableEntityRepository<CarImage> carImagesRepository)
        {
            this.carRepository = carRepository;
            this.carImagesRepository = carImagesRepository;
        }

        public async Task<string> AddImageInBase(IEnumerable<string> images, string carId)
        {
            foreach (var image in images)
            {
                var imageUrl = new CarImage
                {
                    Url = image,
                    CarId = carId,
                };
                await this.carImagesRepository.AddAsync(imageUrl);
                await this.carImagesRepository.SaveChangesAsync();
            }

            return carId;
        }

        public async Task<string> CreateAsync(int year, int modelId, int makeId, int fuelType, int transsmisionType, int cubicCapacity, string registrationNumber, int horsePower, string userId)
        {
            var transsmision = Transmission.ManualGearbox;

            switch (transsmisionType)
            {
                case 2:
                    transsmision = Transmission.SemiAutomatic;
                    break;
                case 3:
                    transsmision = Transmission.Automatic;
                    break;
            }

            var fuelTypeFinal = FuelType.Petrol;

            switch (fuelType)
            {
                case 2:
                    fuelTypeFinal = FuelType.Diesel;
                    break;
                case 3:
                    fuelTypeFinal = FuelType.Electric;
                    break;
                case 4:
                    fuelTypeFinal = FuelType.Gas;
                    break;
                case 5:
                    fuelTypeFinal = FuelType.Hybrid;
                    break;
            }

            var car = new Car
            {
                Year = year,
                ModelId = modelId,
                MakeId = makeId,
                FuelType = fuelTypeFinal,
                Transmission = transsmision,
                CubicCapacity = cubicCapacity,
                RegistrationNumber = registrationNumber,
                HorsePower = horsePower,
                UserId = userId,
            };

            await this.carRepository.AddAsync(car);
            await this.carRepository.SaveChangesAsync();
            return car.Id;
        }

        public IEnumerable<T> GetAllByUserId<T>(string userId)
        {
            var allCars = this.carRepository.All().Where(a => a.User.Id == userId);

            return allCars.To<T>();
        }

        public List<SelectListItem> GetFuelTypes()
        {
            var types = new List<SelectListItem>
            {
                 new SelectListItem { Text = "Petrol", Value = "1" },
                 new SelectListItem { Text = "Diesel", Value = "2" },
                 new SelectListItem { Text = "Electric", Value = "3" },
                 new SelectListItem { Text = "Gas", Value = "4" },
                 new SelectListItem { Text = "Hybrid", Value = "5" },
            };

            return types;
        }

        public List<SelectListItem> GetTransmission()
        {
            var transmissions = new List<SelectListItem>
            {
                new SelectListItem { Text = "ManualGearbox", Value = "1" },
                new SelectListItem { Text = "SemiAutomatic", Value = "2" },
                new SelectListItem { Text = "Automatic", Value = "3" },
            };

            return transmissions;
        }

        [System.Obsolete]
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
