namespace CarService.Web.ViewModels.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CarService.Data.Models.CarElements;
    using CarService.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class CarCreateViewModel : IMapTo<Car>, IMapFrom<Car>
    {
        [Required]
        public int Year { get; set; }

        [Required]
        public int ModelId { get; set; }

        [Required]
        public int MakeId { get; set; }

        [Required]
        public int FuelType { get; set; }

        public int CubicCapacity { get; set; }

        public int HorsePower { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        public int TransmissionsId { get; set; }

        public ICollection<IFormFile> CarImages { get; set; }
    }
}
