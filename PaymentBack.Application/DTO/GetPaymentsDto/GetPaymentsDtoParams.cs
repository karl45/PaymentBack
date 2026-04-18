using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentBack.Application.DTO.GetPaymentsDto
{
    public class GetPaymentsDtoParams
    {
        public int LastId { set; get; } = 0;

        public int PrevId { set; get; } = 0;

        public int PageSize { set; get; } = 10;
    }
}
