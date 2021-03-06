﻿namespace CarService.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarService.Data.Models;
    using CarService.Data.Models.CarRepair;
    using CarService.Services.Data;
    using CarService.Services.Data.RepairServices;
    using CarService.Web.ViewModels.Repair;
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class RepairController : BaseController
    {
        private readonly IRepairService repairService;
        private readonly ICarService carservice;
        private readonly IRepairImageService repairImageService;
        private readonly ICommentsService commentService;
        private readonly IMakesService makesService;
        private readonly IModelsService modelsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Cloudinary cloudinary;

        public RepairController(
            IRepairService repairService,
            ICarService carservice,
            IRepairImageService repairImageService,
            ICommentsService commentService,
            IMakesService makesService,
            IModelsService modelsService,
            UserManager<ApplicationUser> userManager,
            Cloudinary cloudinary)
        {
            this.repairService = repairService;
            this.carservice = carservice;
            this.repairImageService = repairImageService;
            this.commentService = commentService;
            this.makesService = makesService;
            this.modelsService = modelsService;
            this.userManager = userManager;
            this.cloudinary = cloudinary;
        }

        public IActionResult All(int page = 1, string search = null)
        {
            var repairs = this.repairService.GetAll();

            var listofRepairs = new List<RepairAllViewModel>();

            foreach (var repair in repairs)
            {
                var currentRepar = new RepairAllViewModel
                {
                    Id = repair.Id,
                    RepairType = repair.RepairType.ToString(),
                    Description = repair.Description,
                    CarRegistrationNumber = this.carservice.GetById(repair.CarId).RegistrationNumber,
                    RepairImageUrl = this.repairImageService.GetImageUrlByRepairId(repair.Id),
                };

                listofRepairs.Add(currentRepar);
            }

            return this.View(listofRepairs);
        }

        public IActionResult ByType(string type)
        {
            var repairs = this.repairService.ByType(type);

            var listofRepairs = new List<RepairAllViewModel>();

            foreach (var repair in repairs)
            {
                var currentRepar = new RepairAllViewModel
                {
                    Id = repair.Id,
                    RepairType = repair.RepairType.ToString(),
                    Description = repair.Description,
                    CarRegistrationNumber = this.carservice.GetById(repair.CarId).RegistrationNumber,
                    RepairImageUrl = this.repairImageService.GetImageUrlByRepairId(repair.Id),
                };

                listofRepairs.Add(currentRepar);
            }

            return this.View(listofRepairs);
        }

        public IActionResult ByCar(string carId)
        {
            var repairs = this.repairService.ByCar(carId);

            var listofRepairs = new List<RepairAllViewModel>();

            foreach (var repair in repairs)
            {
                var currentRepar = new RepairAllViewModel
                {
                    Id = repair.Id,
                    RepairType = repair.RepairType.ToString(),
                    Description = repair.Description,
                    CarRegistrationNumber = this.carservice.GetById(repair.CarId).RegistrationNumber,
                    RepairImageUrl = this.repairImageService.GetImageUrlByRepairId(repair.Id),
                };

                listofRepairs.Add(currentRepar);
            }

            return this.View(listofRepairs);
        }

        public IActionResult ById(string id)
        {
            var repairViewModel = this.repairService.GetById<RepairViewModel>(id);

            if (repairViewModel == null)
            {
                return this.NotFound();
            }

            if (repairViewModel.VotesCount < 0)
            {
                repairViewModel.VotesCount = 0;
            }

            repairViewModel.Images = this.repairImageService.GetImagesUrlsByRepairId(id);

            repairViewModel.RepairTypeByString = ((RepairType)repairViewModel.RepairType).ToString();

            var car = this.carservice.GetById(repairViewModel.CarId);

            repairViewModel.CarMake = this.makesService.GetNameById(car.MakeId);
            repairViewModel.CarModel = this.modelsService.GetNameById(car.ModelId);

            return this.View(repairViewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            var cars = this.carservice.GetAll<CarsDropDownViewModel>();
            var repairTypes = this.repairService.GetRepairTypes();

            var model = new CreateRepairViewModel
            {
                Cars = cars,
                RepairType = repairTypes,
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateRepairPostViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var files = model.RepairImages;

            var carId = await this.repairService.CreateAsync(
                model.Date,
                model.Description,
                model.RepairTypeId,
                model.Kilometers,
                model.CarId);

            if (files != null)
            {
                var urlOfProducts = await this.repairImageService.UploadAsync(this.cloudinary, files);
                await this.repairImageService.AddImageInBase(urlOfProducts, carId);
            }

            return this.Redirect("Home");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var viewModel = new RepairDeleteModel
            {
                Id = id,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(RepairDeleteModel input)
        {
            var repair = this.repairService.GetById<RepairViewModel>(input.Id);

            var carId = repair.CarId;

            var repairUserId = this.carservice.GetById(carId).UserId;

            var currentUser = await this.userManager.GetUserAsync(this.User);

            var currentUserId = currentUser.Id;

            if (currentUserId != repairUserId)
            {
                return this.BadRequest("Failed to delete the repair");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.repairService.Delete(input.Id);

            return this.RedirectToAction("All");
        }
    }
}
