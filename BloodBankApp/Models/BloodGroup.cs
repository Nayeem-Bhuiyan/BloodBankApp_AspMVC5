using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BloodBankApp.Models
{
    public partial class BloodGroup
    {
        [Key]
        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public virtual ICollection<BloodRequest> BloodRequests { get; set; }
        public virtual ICollection<NewRegistration> NewRegistrations { get; set; }
    }
}