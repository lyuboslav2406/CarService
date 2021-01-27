namespace CarService.Data.Models.CarElements
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CarService.Data.Common.Models;

    public class CarImage : BaseDeletableModel<string>
    {
        public CarImage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Url { get; set; }

        [Required]
        public string CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
