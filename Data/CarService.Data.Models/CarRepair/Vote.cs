namespace CarService.Data.Models.CarRepair
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CarService.Data.Common.Models;

    public class Vote : BaseDeletableModel<string>
    {
        public Vote()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string RepairId { get; set; }

        public virtual Repair Repair { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public VoteType Type { get; set; }
    }
}
