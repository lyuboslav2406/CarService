namespace CarService.Data.Models.CarRepair
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CarService.Data.Common.Models;

    public class RepairImage : BaseDeletableModel<string>
    {
        public RepairImage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Url { get; set; }

        [Required]
        public string RepairId { get; set; }

        public virtual Repair Repair { get; set; }
    }
}
