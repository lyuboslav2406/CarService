namespace CarService.Data.Models.CarRepair
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CarService.Data.Common.Models;
    using CarService.Data.Models.CarElements;

    public class Repair : BaseModel<string>
    {
        public Repair()
        {
            this.Id = Guid.NewGuid().ToString();
            this.RepairImages = new HashSet<RepairImage>();
        }

        [Required]
        public DateTime Date { get; set; }

        public string Description { get; set; }

        [Required]
        public RepairType RepairType { get; set; }

        public int Kilometers { get; set; }

        [Required]
        public string CarId { get; set; }

        public Car Car { get; set; }

        public virtual ICollection<RepairImage> RepairImages { get; set; }
    }
}
