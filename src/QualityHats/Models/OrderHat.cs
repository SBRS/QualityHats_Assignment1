using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualityHats.Models
{
    public class OrderHat
    {
        public int HatID { get; set; }
        public int OrderID { get; set; }
        public int Quantity { get; set; }

        public Hat Hat { get; set; }
        public Order Order { get; set; }
    }
}
