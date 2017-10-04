using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QualityHats.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string Status { get; set; }
        [Column (TypeName = "money")]
        public decimal Subtotal { get; set; }
        [Column(TypeName = "money")]
        public decimal GST { get; set; }
        [Column(TypeName = "money")]
        public decimal GrandTotal { get; set; }
        public System.DateTime OrderDate { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
        public ApplicationUser User { get; set; }        
    }
}
