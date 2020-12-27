namespace CarService.Web.ViewModels.Repair
{
    using System.Net;
    using System.Text.RegularExpressions;

    using CarService.Data.Models.CarRepair;
    using CarService.Services.Mapping;

    public class RepairAllViewModel : IMapTo<Repair>, IMapFrom<Repair>
    {
        public string Id { get; set; }

        public string CarRegistrationNumber { get; set; }

        public string RepairImageUrl { get; set; }

        public string RepairType { get; set; }

        public string Description { get; set; } = "The repair description is empty";

        public string ShortDescription
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Description, @"<[^>]+>", string.Empty));
                return content.Length > 300
                        ? content.Substring(0, 300) + "..."
                        : content;
            }
        }

        public string CarId { get; set; }
    }
}
