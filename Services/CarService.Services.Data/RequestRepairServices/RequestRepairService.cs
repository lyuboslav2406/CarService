namespace CarService.Services.Data.RequestRepairServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using global::CarService.Data.Common.Repositories;
    using global::CarService.Data.Models;

    public class RequestRepairService : IRequestRepairService
    {
        private readonly IDeletableEntityRepository<RepairRequest> requestRepairRepository;

        public RequestRepairService(IDeletableEntityRepository<RepairRequest> requestRepairRepository)
        {
            this.requestRepairRepository = requestRepairRepository;
        }

        public async Task<string> CreateAsync(DateTime date, string description, string userId)
        {
            var requestRepair = new RepairRequest
            {
                Date = date,
                Description = description,
                UserId = userId,
            };

            await this.requestRepairRepository.AddAsync(requestRepair);
            await this.requestRepairRepository.SaveChangesAsync();


            return userId;
        }

        public IList<RepairRequest> GetAll()
        {
            var repairs = this.requestRepairRepository.All().ToList();

            return repairs;
        }
    }
}
