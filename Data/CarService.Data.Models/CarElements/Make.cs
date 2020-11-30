using System.ComponentModel.DataAnnotations;

namespace CarService.Data.Models.CarElements
{
    public class Make
    {
        [Required]
        public int Id { get; set; }

        public string Code { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
