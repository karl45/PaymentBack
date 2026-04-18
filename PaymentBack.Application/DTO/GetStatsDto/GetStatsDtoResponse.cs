using PaymentBack.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentBack.Application.DTO.GetStatsDto
{
    public class GetStatsDtoResponse
    {
        public PaymentCommonStatsModel PaymentCommonStats { get; set; }
    }
}
