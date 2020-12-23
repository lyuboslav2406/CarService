namespace CarService.Web.ViewModels.Repair
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateRepairPostViewModel
    {
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int RepairTypeId { get; set; }

        public int Kilometers { get; set; }

        [Required]
        public string CarId { get; set; }

        public ICollection<IFormFile> RepairImages { get; set; }
    }
}
