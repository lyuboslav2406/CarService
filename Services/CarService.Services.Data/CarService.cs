namespace CarService.Services.Data
{
    using System.Collections;
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

        public async Task Delete(string carId)
        {
            var imagesToDelete = this.carImagesRepository.All().Where(i => i.CarId == carId);

            foreach (var image in imagesToDelete)
            {
                this.carImagesRepository.Delete(image);
                this.carImagesRepository.SaveChangesAsync();
            }

            var carToDelete = this.carRepository.All().Where(x => x.Id == carId).FirstOrDefault();

            this.carRepository.HardDelete(carToDelete);
            await this.carRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Car> query =
                this.carRepository.All().OrderBy(x => x.RegistrationNumber);

            return query.To<T>().ToList();
        }

        public IList<Car> GetAllByUserId(string userId)
        {
            var allCars = this.carRepository.All().Where(x => x.UserId == userId).ToList();

            return allCars;
        }

        public Car GetById(string id)
        {
            var car = this.carRepository.All().Where(x => x.Id == id).FirstOrDefault();
            return car;
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
    }
}
