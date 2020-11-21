using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankApp.ViewModel
{
    public class VmSearchParameter_Registration
    {
        public int RegId { get; set; }
        public string EmailNumber { get; set; }
        public string Password    { get; set; }
        public string DonorName   { get; set; }
        public string Phone       { get; set; }
        public string Address     { get; set; }
        public string Qualification { get; set; }
        public byte[] Photo { get; set; }
        public string ImageUrl { get; set; }
        public int Age { get; set; }

        public int GenderId { get; set; }
        public string GenderName { get; set; }

        public int GroupId { get; set; }
        public string GroupName { get; set; }


    }
}