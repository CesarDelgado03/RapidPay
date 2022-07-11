using Microsoft.EntityFrameworkCore;
using RapidPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidPay.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        public DbSet<CreditCard> creditCards { get; set; }
        public DbSet<PaymentFees> paymentFees { get; set; }
        public DbSet<PaymentsLog> paymentsLogs { get; set; }

    }
}
