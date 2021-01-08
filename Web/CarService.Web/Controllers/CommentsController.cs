namespace CarService.Web.Controllers
{
    using System.Threading.Tasks;

    using CarService.Data.Models;
    using CarService.Services.Data.RepairServices;
    using CarService.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : BaseController
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(
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
                input.ParentId == string.Empty ? null : input.ParentId;

            if (parentId != string.Empty)
            {
                if (!this.commentsService.IsInPostId(parentId, input.RepairId))
                {
                    return this.BadRequest();
                }
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.commentsService.Create(input.RepairId, userId, input.Content, parentId);

            return this.RedirectToAction("RepairId", "Repairs", new { id = input.RepairId });
        }
    }
}
