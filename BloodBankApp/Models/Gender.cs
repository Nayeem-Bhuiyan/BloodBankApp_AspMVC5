using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankApp.Models
{

    public partial class Gender
    {

        public int GenderId { get; set; }
        public string GenderName { get; set; }

        public virtual ICollection<BloodRequest> BloodRequests { get; set; }
        public virtual ICollection<NewRegistration> NewRegistrations { get; set; }
    }

}