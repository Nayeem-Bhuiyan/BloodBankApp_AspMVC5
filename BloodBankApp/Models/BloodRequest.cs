using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankApp.Models
{
    public partial class BloodRequest
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int PatientAge { get; set; }
        public string Problem { get; set; }
        public string Address { get; set; }
        public string HospitalName { get; set; }
        public System.DateTime BloodRequestDate { get; set; }
        public System.DateTime BloodNeedDate { get; set; }
        public int Countity { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoPathUrl { get; set; }

        public int GroupId { get; set; }
        public int GenderId { get; set; }

        public virtual BloodGroup BloodGroup { get; set; }
        public virtual Gender Gender { get; set; }
    }
}