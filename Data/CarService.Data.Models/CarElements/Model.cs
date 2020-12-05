using CarService.Data.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace CarService.Data.Models.CarElements
{
    public class Model : BaseDeletableModel<int>
    {
        public Make Make { get; set; }

        [Required]
        public int MakeId { get; set; }

        public string Code { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
