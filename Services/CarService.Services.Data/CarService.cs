namespace CarService.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using global::CarService.Data.Common.Repositories;
    using global::CarService.Data.Models.CarElements;

    public class CarService : ICarService
    {
        private readonly IDeletableEntityRepository<Car> carRepository;

        public CarService(IDeletableEntityRepository<Car> carRepository)
        {
            this.carRepository = carRepository;
        }

        public async Task<string> CreateAsync(Car car)
        {
            await this.carRepository.AddAsync(car);
            await this.carRepository.SaveChangesAsync();
            return car.Id;
        }

        public List<string> GetFuelTypes()
        {
            string[] authors =
            {
                "Petrol", "Diesel",
                "Electric", "Gas", "Hybrid",
            };

            var list = authors.ToList();

            return list;

        }
    }
}
