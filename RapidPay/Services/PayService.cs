using RapidPay.Data;
using RapidPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidPay.Services
{
    public class PayService
    {
        private readonly ApplicationDbContext _context;

        public PayService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<double> GetCurrentFee()
        {
            PaymentFees paymentFee = GetPaymentFees().FirstOrDefault(); 
            if (paymentFee == null)
            {
                PaymentFees newPaymentFee = new PaymentFees();
                newPaymentFee.currentFee = RandomDecimal(0, 2);
                newPaymentFee.lastFee = 0;
                newPaymentFee.transactionDate = DateTime.Now;
                await SavePaymentFee(newPaymentFee);
                return newPaymentFee.currentFee;
            }
            else
            {
                if ((DateTime.Now - paymentFee.transactionDate).TotalHours >= 1)
                {
                    paymentFee.lastFee = paymentFee.currentFee;
                    paymentFee.currentFee *= RandomDecimal(0, 2);
                    await UpdatePaymentFee(paymentFee);
                }
            }
            return paymentFee.currentFee;
        }

        public List<PaymentFees> GetPaymentFees()
        {
            var payList = _context.paymentFees;
            return payList == null ? null : payList.ToList();
        }

        public async Task SavePaymentFee(PaymentFees model)
        {
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePaymentFee(PaymentFees model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
        }



        private double RandomDecimal(int minRange, int maxRange)
        {
            Random random = new Random();
            string result = $"{random.Next(minRange, maxRange)}.{random.Next(0, 9)}";
            return Convert.ToDouble(result);
        }
    }
}
