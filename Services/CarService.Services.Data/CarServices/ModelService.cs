namespace CarService.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

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

        public string GetNameById(int id)
        {
            var name = this.modelsRepository.All().Where(a => a.Id == id).FirstOrDefault().Title;

            return name;
        }
    }
}
