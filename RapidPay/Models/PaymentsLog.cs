using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidPay.Models
{
    public class PaymentsLog
    {
        public Guid id { get; set; }
        public long cardId { get; set; }
        public double fee { get; set; }
        public double amount { get; set; }
        public double cardBalence { get; set; }
        public DateTime transactionDate { get; set; }
    }
}
