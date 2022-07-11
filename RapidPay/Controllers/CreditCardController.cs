using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Data;
using RapidPay.Helpers;
using RapidPay.Models;
using RapidPay.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RapidPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuthentication]
    public class CreditCardController : ControllerBase
    {
        private readonly CardServices _cardService;
        private readonly CardHelper cardHelper = new CardHelper();
        private ApplicationDbContext _context;
        

        public CreditCardController(CardServices cardService, ApplicationDbContext context)
        {
            _cardService = cardService;
            _context = context;
        }

        [Route("GetCard/{cardNumber}")]
        [HttpGet]
        public IActionResult GetCardBalance(long cardNumber)
        {
            try
            {
                var creditCard = _cardService.GetCardByCardNumber(cardNumber);
                if (creditCard != null)
                {
                    return Ok(creditCard);
                }
                else
                {
                    return StatusCode(400, new
                    {
                        ErrKey = 400,
                        ErrMsg = "Card not Found"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    ErrKey = 500,
                    ErrMsg = ex.Message
                });
            }
            
        }

        [Route("CreateCard/{userId}/{amount}")]
        [HttpPost]
        public async Task<IActionResult> CreateCard(int userId, double amount)
        {

            try
            {
                CreditCard card = new CreditCard();
                card.cardId = _cardService.GetValidCardCode();
                card.amount = amount;
                card.userId = userId;
                card.balance = amount;

                string message = cardHelper.ValidateCard(card);
                if (string.IsNullOrEmpty(message))
                {
                    await _cardService.SaveCreditCard(card);
                    return Ok();
                }
                else
                {
                    return StatusCode(400, new
                    {
                        ErrKey = 400,
                        ErrMsg = message
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    ErrKey = 500,
                    ErrMsg = ex.Message
                });
            }

            
        }

        [Route("PayToCard/{cardNumber}/{amount}")]
        [HttpPost]
        public async Task<IActionResult> PayToCard(long cardNumber, double amount)
        {
            try
            {
                var creditCard = _cardService.GetCardByCardNumber(cardNumber);
                if (creditCard == null)
                {
                    return StatusCode(400, new
                    {
                        ErrKey = 400,
                        ErrMsg = "Card not Found"
                    });
                }
                else if (amount <= 0)
                {
                    return StatusCode(400, new
                    {
                        ErrKey = 400,
                        ErrMsg = "Amount must be greater than 0"
                    });
                }
                else if (creditCard.balance < amount)
                {
                    return StatusCode(400, new
                    {
                        ErrKey = 400,
                        ErrMsg = "Not enough balance for transaction"
                    });
                }
                else
                {
                    await CardSingleton.GetInstance(_context).PaymentProcess(creditCard, amount);
                    return Ok(creditCard);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    ErrKey = 500,
                    ErrMsg = ex.Message
                });
            }
            
        }

    }
}
