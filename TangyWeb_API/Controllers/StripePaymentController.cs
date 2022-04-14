﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Tangy_Models;

namespace TangyWeb_API.Controllers
{
  [Route("api/[controller]/[action]")]
  public class StripePaymentController : Controller
  {
    private readonly IConfiguration _configuration;

    public StripePaymentController(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    [HttpPost]
    [Authorize]
    [ActionName("Create")]
    public async Task<IActionResult> Create([FromBody] StripePaymentDTO paymentDto)
    {
      try
      {
        var domain = _configuration.GetValue<string>("Tangy_Client_URL");
        var options = new SessionCreateOptions
        {
          SuccessUrl = domain+paymentDto.SuccessUrl,
          CancelUrl = domain + paymentDto.CancelUrl,
          LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
       };

        foreach (var item in paymentDto.Order.OrderDetails)
        {
          var sessionLineItem = new SessionLineItemOptions
          {
            PriceData = new SessionLineItemPriceDataOptions
            {
              UnitAmount = (long)(item.Price * 100), // 10.12 $ to 1012
              Currency = "usd",
              ProductData = new SessionLineItemPriceDataProductDataOptions
              {
                Name = item.Product.Name
              }
            },
            Quantity = item.Count
          };
          options.LineItems.Add(sessionLineItem);
        }
        var service = new SessionService();
        Session session = service.Create(options);
        return Ok(new SuccessModelDTO()
        {
          Data = session.Id+";"+session.PaymentIntentId
        });
      }
      catch (Exception e)
      {
        return BadRequest(new ErrorModelDTO()
        {
          ErrorMessage = e.Message,
        });
      }
    }
  }
}