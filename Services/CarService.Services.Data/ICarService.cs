namespace CarService.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using global::CarService.Data.Models.CarElements;

    public interface ICarService
    {
        Task<string> CreateAsync(Car car);

        public List<string> GetFuelTypes();

        //IEnumerable<T> GetAll<T>();
    }
}
