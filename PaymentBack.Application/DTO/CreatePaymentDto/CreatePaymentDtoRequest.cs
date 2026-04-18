using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentBack.Application.DTO.CreatePaymentDto
{
    public class CreatePaymentDtoRequest
    {
        public required PaymentModel Payment { get; set; }
    }
}
