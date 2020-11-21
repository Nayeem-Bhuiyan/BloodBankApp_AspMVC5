using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BloodBankApp.Models
{
    public partial class Admin
    {
        
        public int id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}