namespace CarService.Services.Data.RequestRepairServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using global::CarService.Data.Models;

    public interface IRequestRepairService
    {
        public Task<string> CreateAsync(
           DateTime date,
           string description,
           string userId);

        IList<RepairRequest> GetAll();
    }
}
