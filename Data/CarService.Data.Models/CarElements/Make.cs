namespace CarService.Data.Models.CarElements
{
    using System.ComponentModel.DataAnnotations;

    using CarService.Data.Common.Models;

    public class Make : BaseDeletableModel<int>
    {
        public string Code { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
