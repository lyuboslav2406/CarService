namespace CarService.Web.ViewModels.Cars
{
    using CarService.Data.Models.CarElements;
    using CarService.Services.Mapping;

    public class MakesDropDownViewModel : IMapFrom<Make>, IMapTo<Make>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
