using PaymentBack.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentBack.Application.DTO.CreatePaymentDto
{
    public class CreatePaymentDtoResponse
    {
        public PaymentModel Payment { get; set; }
    }
}
