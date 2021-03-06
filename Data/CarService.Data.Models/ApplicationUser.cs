﻿// ReSharper disable VirtualMemberCallInConstructor
namespace CarService.Data.Models
{
    using System;
    using System.Collections.Generic;

    using CarService.Data.Common.Models;
    using CarService.Data.Models.CarElements;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Cars = new HashSet<Car>();
            this.RepairRequests = new HashSet<RepairRequest>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Car> Cars { get; set; }

        public ICollection<RepairRequest> RepairRequests { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public static implicit operator string(ApplicationUser v)
        {
            throw new NotImplementedException();
        }
    }
}
