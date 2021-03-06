﻿namespace CarService.Web.ViewModels.Cars
{
    using CarService.Data.Models.CarElements;
    using CarService.Services.Mapping;

    public class ModelsDropDrownViewModel : IMapFrom<Model>, IMapTo<Model>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}