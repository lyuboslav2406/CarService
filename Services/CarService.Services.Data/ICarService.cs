namespace CarService.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using global::CarService.Data.Models.CarElements;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ICarService
    {
        public Task<string> CreateAsync(
            int year,
            int modelId,
            int makeId,
            int fuelType,
            int transsmisionType,
            int cubicCapacity,
            string registrationNumber,
            int horsePower,
            string userId);

        Car GetById(string id);

        IEnumerable<T> GetAll<T>();

        Task Delete(string carId);

        IList<Car> GetAllByUserId(string userId);

        public List<SelectListItem> GetFuelTypes();

        public List<SelectListItem> GetTransmission();
    }
}