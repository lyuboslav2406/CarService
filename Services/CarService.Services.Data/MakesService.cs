namespace CarService.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using global::CarService.Data.Common.Repositories;
    using global::CarService.Data.Models.CarElements;
    using global::CarService.Services.Mapping;

    public class MakesService : IMakesService
    {
        private readonly IDeletableEntityRepository<Make> makesRepository;

        public MakesService(IDeletableEntityRepository<Make> makesRepository)
        {
            this.makesRepository = makesRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Make> query =
                this.makesRepository.All().OrderBy(x => x.Title);

            return query.To<T>().ToList();
        }
    }
}
