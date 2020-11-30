using CarService.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarService.Data.Models.CarRepair
{
    public class Vote : BaseModel<string>
    {
        public Vote()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public int RepairId { get; set; }

        public virtual Repair Repair { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public VoteType Type { get; set; }
    }
}
