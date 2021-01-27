namespace CarService.Web.ViewModels.Repair
{
    using System;

    using CarService.Data.Models.CarRepair;
    using CarService.Services.Mapping;
    using Ganss.XSS;

    public class RepairCommentViewModel : IMapFrom<RepairComment>
    {
        public string Id { get; set; }

        public string ParentId { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public DateTime CreatedOn { get; set; }

        public string UserUserName { get; set; }
    }
}
