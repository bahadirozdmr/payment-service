// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Payment.Dto;

namespace payment_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IConfiguration _configuration;

        public PaymentController(ILogger<PaymentController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("{paymentId}")]
        public ActionResult GetPayment(Guid paymentId)
        {
            if (paymentId == Guid.Empty)
            {
                return NotFound(paymentId);
            }

            var payment = new PaymentDto()
            {
                PaymentId = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                Message = _configuration.GetSection("PaymentGateway:Message").Value
            };
            return Ok(payment);
        }

        [HttpGet()]
        public ActionResult GetPayments()
        {
            var response = new PaymentDto() {PaymentId = Guid.NewGuid(), CreateDate = DateTime.Now};
            var message=_configuration.GetSection("PaymentGateway:Message").Value;
            response.Message = message;
            return Ok(response);
        }

        [HttpPost()]
        public ActionResult CreatePayments(PaymentDto request)
        {
            if (request.PaymentId != Guid.Empty)
            {
                return BadRequest(request);
            }

            return Created("/created",request);
        }
    }
}
