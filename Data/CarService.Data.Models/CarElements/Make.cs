using CarService.Data.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace CarService.Data.Models.CarElements
{
    public class Make : BaseDeletableModel<int>
    {
        public string Code { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
