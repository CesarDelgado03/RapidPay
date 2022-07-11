using RapidPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.Helpers
{
    public class CardHelper
    {
        public long GenerateCardToken()
        {
            int[] checkArray = new int[14];

            var cardNum = new int[15];
            Random _random = new Random();

            for (int d = 13; d >= 0; d--)
            {
                cardNum[d] = _random.Next(0, 9);
                checkArray[d] = (cardNum[d] * (((d + 1) % 2) + 1)) % 9;
            }

            cardNum[14] = (checkArray.Sum() * 9) % 10;

            var sb = new StringBuilder();

            for (int d = 0; d < 15; d++)
            {
                sb.Append(cardNum[d].ToString());
            }
            return long.Parse(sb.ToString());
        }

        public string ValidateCard(CreditCard model)
        {
            string message = string.Empty;
            if (string.IsNullOrEmpty(model.userId.ToString()))
            {
                message = "Invalid user";
            }
            else if (model.amount < 500)
            {
                message = "Invalid card Amount";
            }

            return message;
        }
    }
}
