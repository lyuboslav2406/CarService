namespace CarService.Web.ViewModels.Repair
{
    using CarService.Data.Models.CarElements;
    using CarService.Services.Mapping;

    public class CarsDropDownViewModel : IMapFrom<Car>, IMapTo<Car>
    {
        public string Id { get; set; }

        public string RegistrationNumber { get; set; }
    }
}