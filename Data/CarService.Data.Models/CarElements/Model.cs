namespace CarService.Data.Models.CarElements
{
    using System.ComponentModel.DataAnnotations;

    public class Model
    {
        [Required]
        public int Id { get; set; }

        public Make Make { get; set; }

        [Required]
        public int MakeId { get; set; }

        public string Code { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
