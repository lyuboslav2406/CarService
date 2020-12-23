namespace CarService.Data.Models.CarElements
{
    using System.ComponentModel.DataAnnotations;

    using CarService.Data.Common.Models;

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
