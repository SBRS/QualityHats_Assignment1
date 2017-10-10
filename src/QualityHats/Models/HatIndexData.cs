using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualityHats.Models
{
    public class HatIndexData
    {
        public IEnumerable<Hat> Hats { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
