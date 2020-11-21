using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BloodBankApp.Models
{
    public partial class NewRegistration
    {
        [Key]
        public int RegId { get; set; }
        public string EmailNumber { get; set; }
        public string Password { get; set; }
        public string DonorName { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        public byte[] Photo { get; set; }
        public string ImageUrl { get; set; }
        public int GroupId { get; set; }
        public int GenderId { get; set; }

        public virtual BloodGroup BloodGroup { get; set; }
        public virtual Gender Gender { get; set; }
    }
}