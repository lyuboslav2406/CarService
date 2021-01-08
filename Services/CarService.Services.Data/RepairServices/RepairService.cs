namespace CarService.Services.Data.RepairServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using global::CarService.Data.Common.Repositories;
    using global::CarService.Data.Models.CarRepair;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class RepairService : IRepairService
    {
        private readonly IDeletableEntityRepository<Repair> repairRepository;

        public RepairService(IDeletableEntityRepository<Repair> repairRepository)
        {
            this.repairRepository = repairRepository;
        }

        public IList<Repair> ByCar(string carId)
        {
            var repairs = this.repairRepository.All().Where(a => a.CarId == carId).ToList();

            return repairs;
        }

        public IList<Repair> ByType(string type)
        {
            var repairTypeFinal = RepairType.Transmission;

            switch (type)
            {
                case "Engine":
                    repairTypeFinal = RepairType.Engine;
                    break;
                case "Suspension":
                    repairTypeFinal = RepairType.Suspension;
                    break;
                case "Service":
                    repairTypeFinal = RepairType.Service;
                    break;
                case "Clutch":
                    repairTypeFinal = RepairType.Clutch;
                    break;
            }

            var repairs = this.repairRepository.All().Where(a => a.RepairType == repairTypeFinal).ToList();

            return repairs;
        }

        public async Task<string> CreateAsync(
            DateTime date,
            string description,
            int repairType,
            int kilometers,
            string carId)
        {
            var repairTypeFinal = RepairType.Transmission;

            switch (repairType)
            {
                case 2:
                    repairTypeFinal = RepairType.Engine;
                    break;
                case 3:
                    repairTypeFinal = RepairType.Suspension;
                    break;
                case 4:
                    repairTypeFinal = RepairType.Service;
                    break;
                case 5:
                    repairTypeFinal = RepairType.Clutch;
                    break;
            }

            var repair = new Repair
            {
                Date = date,
                Description = description,
                RepairType = repairTypeFinal,
                Kilometers = kilometers,
                CarId = carId,
            };

            await this.repairRepository.AddAsync(repair);
            await this.repairRepository.SaveChangesAsync();
            return repair.Id;
        }

        public IList<Repair> GetAll()
        {
            var repairs = this.repairRepository.All().ToList();

            return repairs;
        }

        public Repair GetById(string id)
        {
            var repair = this.repairRepository.All().Where(x => x.Id == id).FirstOrDefault();

            return repair;
        }

        public List<SelectListItem> GetRepairTypes()
        {
            var types = new List<SelectListItem>
            {
                 new SelectListItem { Text = "Transmission", Value = "1" },
                 new SelectListItem { Text = "Engine", Value = "2" },
                 new SelectListItem { Text = "Suspension", Value = "3" },
                 new SelectListItem { Text = "Service", Value = "4" },
                 new SelectListItem { Text = "Clutch", Value = "5" },
            };

            return types;
        }
    }
}
