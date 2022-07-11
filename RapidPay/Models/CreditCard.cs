using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RapidPay.Models
{
    public class CreditCard
    {
        public CreditCard()
        {
            this.creationDate = DateTime.Now;
            this.expirationDate = DateTime.Now.AddYears(4);
            this.status = 1;
        }
        
        
        public long Id { get; set; }
        [Required]
        public long cardId { get; set; }
        [Required]
        public int userId {get; set;}
        public DateTime creationDate { get; set; }
        public DateTime expirationDate { get; set; }
        [Required]
        public double amount { get; set; }
        public double balance { get; set; }
        /// <summary>
        /// 1 = Active, 2= Inactive
        /// </summary>
        public int status { get; set; }
    }
}
