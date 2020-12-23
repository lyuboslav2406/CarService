namespace CarService.Web.ViewModels.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CarService.Data.Models.CarElements;
    using CarService.Services.Mapping;

    public class AllCarsViewModel : IMapTo<Car>, IMapFrom<Car>
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int ModelId { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int MakeId { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public string CarImage { get; set; }
    }
}
