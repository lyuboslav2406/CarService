

namespace CarService.Web.ViewModels.RepairComments
{
    public class CreateCommentInputModel
    {
        public string RepairId { get; set; }

        public string ParentId { get; set; }

        public string Content { get; set; }
    }
}
