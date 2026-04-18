namespace PaymentBack.Domain.Models
{
    public class PaymentGroupedByDayStatsModel
    {
        public DateTime Date { get; set; }
        public int PaymentCount { get; set; }

        public decimal PaymentTotal { set; get; }
    }
}
