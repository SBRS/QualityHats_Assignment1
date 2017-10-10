using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QualityHats.Models
{
    public class Hat
    {
        public int HatID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [Display(Name = "Hat Name")]
        public string HatName { get; set; }
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }
        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        [Required]
        [Display(Name = "Price")]
        public decimal UnitPrice { get; set; }
        [Required]
        public string Description { get; set; }
        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        public Supplier Supplier { get; set; }
        public Category Category { get; set; }
    }
}
