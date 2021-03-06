﻿namespace CarService.Data.Models.CarRepair
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CarService.Data.Common.Models;

    public class RepairComment : BaseDeletableModel<string>
    {
        public RepairComment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string RepairId { get; set; }

        public virtual Repair Repair { get; set; }

        public string ParentId { get; set; }

        public virtual RepairComment Parent { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
