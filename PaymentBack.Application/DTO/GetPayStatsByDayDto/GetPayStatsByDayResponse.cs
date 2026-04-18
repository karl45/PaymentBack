using PaymentBack.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentBack.Application.DTO.GetPayStatsByDayDto
{
    public class GetPayStatsByDayResponse
    {
        public List<PaymentGroupedByDayStatsModel> DayStats { get; set; }
    }
}
