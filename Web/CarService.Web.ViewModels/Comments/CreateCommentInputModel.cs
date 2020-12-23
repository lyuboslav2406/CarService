namespace CarService.Web.ViewModels.Comments
{
    public class CreateCommentInputModel
    {
        public string RepairId { get; set; }

        public string ParentId { get; set; }

        public string Content { get; set; }
    }
}
