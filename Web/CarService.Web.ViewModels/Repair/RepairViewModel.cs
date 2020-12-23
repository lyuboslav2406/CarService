namespace CarService.Web.ViewModels.Repair
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using AutoMapper;
    using CarService.Data.Models.CarElements;
    using CarService.Data.Models.CarRepair;
    using CarService.Services.Mapping;
    using Ganss.XSS;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class RepairViewModel : IMapTo<Repair>, IMapFrom<Repair>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Description { get; set; } = "The repair description is empty";

        public string SanitizedDescription => new HtmlSanitizer().Sanitize(this.Description);

        public string RepairType { get; set; }

        public List<SelectListItem> RepairTypes { get; set; }

        public int Kilometers { get; set; }

        public string CarId { get; set; }

        public Car Car { get; set; }

        public string UserUserName { get; set; }

        public int VotesCount { get; set; }

        public IEnumerable<RepairComment> Comments { get; set; }

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

        public int CommentsCount { get; set; }

        public string Url => $"/Repair/ById/{this.Id}";

        public ICollection<RepairImage> Images { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Repair, RepairViewModel>()
                .ForMember(x => x.VotesCount, options =>
                {
                    options.MapFrom(p => p.Votes.Sum(v => (int)v.Type));
                });
        }
    }
}
