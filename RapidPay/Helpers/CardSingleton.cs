using RapidPay.Data;
using RapidPay.Models;
using RapidPay.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidPay.Helpers
{
    public class CardSingleton
    {
        private static CardSingleton _instance = null;
        private static ApplicationDbContext _context;

        public CardSingleton(ApplicationDbContext context)
        {
            _context = context;
        }

        public static CardSingleton GetInstance(ApplicationDbContext context)
        {

            if (_instance == null)
            {
                _instance = new CardSingleton(context);
            }
            return _instance;

        }

        public static CardSingleton GetNewInstance(ApplicationDbContext context)
        {
            _instance = null;
            return GetInstance(context);

        }

        public async Task PaymentProcess(CreditCard creditCard, double amount)
        {
            PayService payService = new PayService(_context);
            CardServices cardService = new CardServices(_context);
            PaymentsLog paymentsLog = new PaymentsLog();
            double currentFee = await payService.GetCurrentFee();

            creditCard.balance -= (amount + currentFee);

            paymentsLog.cardId = creditCard.cardId;
            paymentsLog.amount = amount;
            paymentsLog.cardBalence = creditCard.balance;
            paymentsLog.fee = currentFee;
            paymentsLog.transactionDate = DateTime.Now;

            await cardService.UpdateCreditCard(creditCard);
            await cardService.SaveCreditCardLog(paymentsLog);
        }
    }
}
