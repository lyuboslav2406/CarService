namespace CarService.Web.Controllers
{
    using System.Threading.Tasks;

    using CarService.Data.Models;
    using CarService.Services.Data;
    using CarService.Web.ViewModels.Cars;
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CarsController : BaseController
    {
        private readonly ICarService carService;
        private readonly IModelsService modelService;
        private readonly IMakesService makeService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Cloudinary cloudinary;

        public CarsController(
            ICarService carService,
            IModelsService modelService,
            IMakesService makeService,
            UserManager<ApplicationUser> userManager,
            Cloudinary cloudinary)
        {
            this.carService = carService;
            this.modelService = modelService;
            this.makeService = makeService;
            this.userManager = userManager;
            this.cloudinary = cloudinary;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            var models = this.modelService.GetAll<ModelsDropDrownViewModel>();
            var makes = this.makeService.GetAll<MakesDropDownViewModel>();
            var transmissions = this.carService.GetTransmission();
            var fuelTypes = this.carService.GetFuelTypes();

            var model = new CarViewModel
            {
                FuelTypes = fuelTypes,
                Transmissions = transmissions,
                Models = models,
                Makes = makes,
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CarCreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var files = model.CarImages;

            var carId = await this.carService.CreateAsync(
                model.Year,
                model.ModelId,
                model.MakeId,
                model.FuelType,
                model.TransmissionsId,
                model.CubicCapacity,
                model.RegistrationNumber,
                model.HorsePower,
                user.Id);

            if (files != null)
            {
                var urlOfProducts = await this.carService.UploadAsync(this.cloudinary, files);
                await this.carService.AddImageInBase(urlOfProducts, carId);
            }

            return this.Redirect("Home");
        }


        public async Task<IActionResult> MyCars()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new AllCarsViewModel
            {
                Cars = this.carService.GetAllByUserId<CarViewModel>(user.Id),
            };
            ;
            return this.View(viewModel);
        }
    }
}
