using PaymentBack.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentBack.Application.DTO.GetPaymentsDto
{
    public class GetPaymentsDtoResponse
    {
        public List<PaymentModel> Payments { get; set; }
    }
}
