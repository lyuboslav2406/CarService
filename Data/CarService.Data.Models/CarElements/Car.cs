namespace CarService.Data.Models.CarElements
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CarService.Data.Common.Models;

    public class Car : BaseDeletableModel<string>
    {
        public Car()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CarImages = new HashSet<CarImage>();
        }

        [Required]
        public int Year { get; set; }

        public int ModelId { get; set; }

        [Required]
        public Model Model { get; set; }

        public int MakeId { get; set; }

        public Make Make { get; set; }

        [Required]
        public FuelType FuelType { get; set; }

        public int CubicCapacity { get; set; }

        public int HorsePower { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public Transmission Transmission { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<CarImage> CarImages { get; set; }
    }
}
