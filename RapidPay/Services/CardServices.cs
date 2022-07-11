using RapidPay.Data;
using RapidPay.Helpers;
using RapidPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidPay.Services
{
    public class CardServices
    {
        private readonly ApplicationDbContext _context;
        private readonly CardHelper cardHelper = new CardHelper();

        public CardServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public long GetValidCardCode()
        {
            bool exit = true;
            long code = 0;
            do
            {
                code = cardHelper.GenerateCardToken();
                var cardlist =  _context.creditCards.Where(card => card.cardId == code).FirstOrDefault();
                if (cardlist == null)
                    exit = false;
            } while (exit);

            return code;
        }

        public async Task SaveCreditCard(CreditCard creditCard)
        {
            await _context.AddAsync(creditCard);
            await _context.SaveChangesAsync();
        }

        public  CreditCard GetCardByCardNumber(long number)
        {
            return _context.creditCards.Where(card => card.cardId == number).ToList().FirstOrDefault();
        }

        public async Task UpdateCreditCard(CreditCard creditCard)
        {
            _context.Update(creditCard);
            await _context.SaveChangesAsync();
        }

        public async Task SaveCreditCardLog(PaymentsLog paymentsLog)
        {
            await _context.AddAsync(paymentsLog);
            await _context.SaveChangesAsync();
        }
    }
}
