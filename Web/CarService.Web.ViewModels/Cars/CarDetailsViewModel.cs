namespace CarService.Web.ViewModels.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CarService.Data.Models.CarElements;

    public class CarDetailsViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string FuelType { get; set; }

        [Required]
        public int CubicCapacity { get; set; }

        [Required]
        public int HorsePower { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public string Transmission { get; set; }

        public ICollection<CarImage> CarImages { get; set; }
    }
}
