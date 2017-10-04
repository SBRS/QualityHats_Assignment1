using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QualityHats.Models
{
    public class Hat
    {
        public int HatID { get; set; }
        public string HatName { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public Supplier Supplier { get; set; }
        public Category Category { get; set; }
    }
}
