using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace QualityHats.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Enabled { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string HomeNumber { get; set; }
        public string WorkNumber { get; set; }
        [Display(Name = "Customer Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
