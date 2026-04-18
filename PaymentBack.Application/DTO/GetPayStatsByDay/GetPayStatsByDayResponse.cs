using PaymentBack.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentBack.Application.DTO.GetPayStatsByDay
{
    public class GetPayStatsByDayResponse
    {
        public List<PaymentGroupedByDayStatsModel> DayStats { get; set; }
    }
}
