namespace CarService.Services.Data.RepairServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using global::CarService.Data.Models.CarRepair;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IRepairService
    {
        public Task<string> CreateAsync(
            DateTime date,
            string description,
            int repairType,
            int kilometers,
            string carId);

        public List<SelectListItem> GetRepairTypes();

        Repair GetById(string id);

        IList<Repair> GetAll();

        IList<Repair> ByType(string type);

        IList<Repair> ByCar(string carId);
    }
}
