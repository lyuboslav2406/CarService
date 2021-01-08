namespace CarService.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CarService.Common;
    using CarService.Data.Models;
    using CarService.Services.Data.RequestRepairServices;
    using CarService.Services.Messaging;
    using CarService.Web.ViewModels.RequestRepair;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class RequestRepairController : BaseController
    {
        private readonly IRequestRepairService requestRepairService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SendGridEmailSender emailSender;

        public RequestRepairController(
            IRequestRepairService requestRepairService,
            UserManager<ApplicationUser> userManager,
            SendGridEmailSender emailSender)
        {
            this.requestRepairService = requestRepairService;
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {
            var modelRequestRepairs = new List<AllRequestRepairsViewModel>();

            var requestRepairs = this.requestRepairService.GetAll();

            foreach (var requestRepair in requestRepairs)
            {
                var currentRR = new AllRequestRepairsViewModel
                {
                    Id = requestRepair.Id,
                    Date = requestRepair.Date,
                    Description = requestRepair.Description,
                    CreatedOn = requestRepair.CreatedOn,
                    UserUserName = await this.userManager.GetUserNameAsync(await this.userManager.FindByIdAsync(requestRepair.UserId)),
                };

                modelRequestRepairs.Add(currentRR);
            }

            return this.View(modelRequestRepairs);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateRequestRepairViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var currentUser = await this.userManager.GetUserAsync(this.User);
            var currentUserEmail = currentUser.Email.ToString();

            var currentUserId = currentUser.Id;
            _ = await this.requestRepairService.CreateAsync(model.Date, model.Description, currentUserId);

            await this.emailSender.SendEmailAsync(GlobalConstants.AdministratorEmailAdres, GlobalConstants.SystemName, currentUserEmail, GlobalConstants.SendGridSubject, model.Description); ;

            return this.Redirect("Index");
        }
    }
}
