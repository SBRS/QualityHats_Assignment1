using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QualityHats.Models
{
    public enum Status
    {
        Waiting,
        Shipped
    }

    public class Order
    {
        public int OrderID { get; set; }
        [DisplayFormat(NullDisplayText = "No status")]
        public Status? Status { get; set; }
        public decimal Subtotal { get; set; }
        public decimal GST { get; set; }
        public decimal GrandTotal { get; set; }
        public System.DateTime OrderDate { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
        public ApplicationUser User { get; set; }        
    }
}
