using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarService.Web.ViewModels.Repair
{
    public class CreateRepairViewModel
    {
        public DateTime Date { get; set; }

        public string Description { get; set; }

        [Required]
        public List<SelectListItem> RepairType { get; set; }

        public int RepairTypeId { get; set; }

        public int Kilometers { get; set; }

        [Required]
        public string CarId { get; set; }

        public IEnumerable<CarsDropDownViewModel> Cars { get; set; }

        public ICollection<IFormFile> RepairImages { get; set; }
    }
}
