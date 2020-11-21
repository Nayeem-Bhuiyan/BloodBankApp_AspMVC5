using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BloodBankApp.ViewModel
{
    public class VmSearchParameter_BloodRequest
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Problem { get; set; }
        public string Address { get; set; }
        public string HospitalName { get; set; }
        public DateTime BloodRequestDate { get; set; }
        public DateTime BloodNeedDate { get; set; }
        public string GroupName { get; set; }
        public string GenderName { get; set; }
    }
}