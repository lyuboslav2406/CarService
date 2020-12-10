namespace CarService.Web.ViewModels.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CarService.Data.Models.CarElements;
    using CarService.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using static System.Net.Mime.MediaTypeNames;

    public class CarViewModel : IMapTo<Car>, IMapFrom<Car>
    {
        [Required]
        public int Year { get; set; }

        [Required]
        public int ModelId { get; set; }

        [Required]
        public IEnumerable<ModelsDropDrownViewModel> Models { get; set; }

        [Required]
        public int MakeId { get; set; }

        [Required]
        public IEnumerable<MakesDropDownViewModel> Makes { get; set; }

        [Required]
        public int FuelType { get; set; }

        public List<SelectListItem> FuelTypes { get; set; }

        public int CubicCapacity { get; set; }

        public int HorsePower { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        public int TransmissionsId { get; set; }

        [Required]
        public List<SelectListItem> Transmissions { get; set; }

        public ICollection<CarImage> CarImages { get; set; }
    }
}
