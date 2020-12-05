using CarService.Services.Mapping;

namespace CarService.Web.ViewModels.Cars
{
    public class ModelsDropDrownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}