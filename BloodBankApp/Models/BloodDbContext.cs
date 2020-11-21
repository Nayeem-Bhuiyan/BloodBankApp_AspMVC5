using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BloodBankApp.Models
{
    public partial class BloodBankDbContext : DbContext
    {
        public BloodBankDbContext()
            : base("name=BloodBankDbContext")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<BloodGroup> BloodGroups { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<BloodRequest> BloodRequests { get; set; }
        public virtual DbSet<NewRegistration> NewRegistrations { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

 
    }
}