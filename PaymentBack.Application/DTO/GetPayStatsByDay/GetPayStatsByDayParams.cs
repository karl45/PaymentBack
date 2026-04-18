using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentBack.Application.DTO.GetPayStatsByDay
{
    public class GetPayStatsByDayParams
    {

        public DateTime? PrevDate { get; set; } = null;

        public DateTime? LastDate { get; set; } = null;

        public int PageSize { get; set; } = 10;
    }
}
