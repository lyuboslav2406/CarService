namespace CarService.Web.Controllers
{
    using System.Collections.Generic;
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
        private readonly ICarImageService carImageService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Cloudinary cloudinary;

        public CarsController(
            ICarService carService,
            IModelsService modelService,
            IMakesService makeService,
            ICarImageService carImageService,
            UserManager<ApplicationUser> userManager,
            Cloudinary cloudinary)
        {
            this.carService = carService;
            this.modelService = modelService;
            this.makeService = makeService;
            this.carImageService = carImageService;
            this.userManager = userManager;
            this.cloudinary = cloudinary;
        }

        public IActionResult Details(string id)
        {
            var car = this.carService.GetById(id);
            ;
            if (car == null)
            {
                return this.NotFound();
            }

            var images = this.carImageService.GetImagesUrlsByCarId(id);

            var carViewModel = new CarDetailsViewModel
            {
                Id = car.Id,
                Year = car.Year,
                CubicCapacity = car.CubicCapacity,
                HorsePower = car.HorsePower,
                RegistrationNumber = car.RegistrationNumber,
                Make = this.makeService.GetNameById(car.MakeId),
                Model = this.modelService.GetNameById(car.ModelId),
                FuelType = car.FuelType.ToString(),
                Transmission = car.Transmission.ToString(),
                CarImages = images,
            };

            return this.View(carViewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var car = this.carService.GetById(id);

            var carUserId = car.UserId;

            var currentUser = await this.userManager.GetUserAsync(this.User);

            var currentUserId = currentUser.Id;

            if (currentUserId != carUserId)
            {
                return this.BadRequest("Failed to delete the post");
            }

            var viewModel = new CarDeleteViewModel
            {
                Id = id,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(CarDeleteViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.carService.Delete(input.Id);

            return this.RedirectToAction("MyCars");
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
                var urlOfProducts = await this.carImageService.UploadAsync(this.cloudinary, files);
                await this.carImageService.AddImageInBase(urlOfProducts, carId);
            }

            return this.Redirect("MyCars");
        }

        [Authorize]
        public async Task<IActionResult> MyCars()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var cars = this.carService.GetAllByUserId(user.Id);

            var list = new List<AllCarsViewModel>();

            foreach (var car in cars)
            {
                var currentCar = new AllCarsViewModel
                {
                    Id = car.Id,
                    Make = this.makeService.GetNameById(car.MakeId),
                    Model = this.modelService.GetNameById(car.ModelId),
                    RegistrationNumber = car.RegistrationNumber,
                    CarImage = this.carImageService.GetImageUrlByCarId(car.Id),
                };

                list.Add(currentCar);
            }

            ;
            return this.View(list);
        }
    }
}