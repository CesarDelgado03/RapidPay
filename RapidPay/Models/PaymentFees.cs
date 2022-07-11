using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidPay.Models
{
    public class PaymentFees
    {
        public Guid id { get; set; }
        public double lastFee { get; set; }
        public double currentFee { get; set; }
        public DateTime transactionDate { get; set; }
    }
}
