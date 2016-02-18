using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain
{
    public class Sale
    {
        public decimal Amount { get; set; }
        public bool Bonus { get; set; }
        public bool IsPromotion { get; set; }
    }
}
