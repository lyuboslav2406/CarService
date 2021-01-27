namespace CarService.Web.Controllers
{
    using System.Threading.Tasks;

    using CarService.Data.Models;
    using CarService.Services.Data.RepairServices;
    using CarService.Web.ViewModels.RepairComments;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class RepairCommentsController : BaseController
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public RepairCommentsController(
            ICommentsService commentsService,
            UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentInputModel input)
        {
            var parentId =
                input.ParentId == string.Empty ? string.Empty : input.ParentId;

            //if (parentId.HasValue)
            //{
            //    if (!this.commentsService.is(parentId.Value, input.RepairId))
            //    {
            //        return this.BadRequest();
            //    }
            //}

            var userId = this.userManager.GetUserId(this.User);
            await this.commentsService.Create(input.RepairId, userId, input.Content, parentId);

            return this.RedirectToAction("ById", "Repair", new { id = input.RepairId });
        }
    }
}
