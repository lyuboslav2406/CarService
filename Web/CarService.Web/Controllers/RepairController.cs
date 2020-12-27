namespace CarService.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarService.Data.Models;
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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Cloudinary cloudinary;

        public RepairController(
            IRepairService repairService,
            ICarService carservice,
            IRepairImageService repairImageService,
            ICommentsService commentService,
            UserManager<ApplicationUser> userManager,
            Cloudinary cloudinary)
        {
            this.repairService = repairService;
            this.carservice = carservice;
            this.repairImageService = repairImageService;
            this.commentService = commentService;
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

        public IActionResult ById(string id)
        {
            var repair = this.repairService.GetById(id);

            var repairViewModel = new RepairViewModel
            {
                Id = repair.Id,
                CarId = repair.CarId,
                Comments = this.commentService.CommentsByRepairId(id),
                //VotesCount = repair.Votes.Count(),
                //CommentsCount = repair.Comments.Count(),
                CreatedOn = repair.CreatedOn,
                Description = repair.Description,
                Kilometers = repair.Kilometers,
                Images = this.repairImageService.GetImagesUrlsByRepairId(id),
                RepairType = repair.RepairType.ToString(),
                UserUserName = this.HttpContext.User.Identity.Name,
            };

            if (repairViewModel == null)
            {
                return this.NotFound();
            }

            if (repairViewModel.VotesCount < 0)
            {
                repairViewModel.VotesCount = 0;
            }

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

            ;

            return this.Redirect("Home");
        }
    }
}
