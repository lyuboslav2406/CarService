namespace CarService.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using global::CarService.Data.Common.Repositories;
    using global::CarService.Data.Models.CarElements;
    using global::CarService.Services.Mapping;

    public class ModelService : IModelsService
    {
        private readonly IDeletableEntityRepository<Model> modelsRepository;

        public ModelService(IDeletableEntityRepository<Model> modelsRepository)
        {
            this.modelsRepository = modelsRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Model> query =
                this.modelsRepository.All().OrderBy(x => x.Title);

            return query.To<T>().ToList();
        }
    }
}
