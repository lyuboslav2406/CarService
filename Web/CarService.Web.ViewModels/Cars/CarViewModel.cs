namespace CarService.Web.ViewModels.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CarViewModel
    {
        [Required]
        public int Year { get; set; }

        [Required]
        public int ModelId { get; set; }

        [Required]
        public IEnumerable<ModelsDropDrownViewModel> Models { get; set; }

        [Required]
        public int FuelType { get; set; }

        [Required]
        public IEnumerable<string> FuelTypes { get; set; }

        public int CubicCapacity { get; set; }

        public int HorsePower { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        public int TransmissionsId { get; set; }

        [Required]
        public IEnumerable<FuelTypesDropDownViewModel> Transmissions { get; set; }

        public string CarImage { get; set; }
    }
}
