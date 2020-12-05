namespace CarService.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CarService.Data.Common.Models;

    public class RepairRequest : BaseDeletableModel<string>
    {
        public RepairRequest()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
