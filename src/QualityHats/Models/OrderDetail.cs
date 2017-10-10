using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QualityHats.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Price")]
        public decimal UnitPrice { get; set; }

        public Hat Hat { get; set; }
        public Order Order { get; set; }
    }
}
