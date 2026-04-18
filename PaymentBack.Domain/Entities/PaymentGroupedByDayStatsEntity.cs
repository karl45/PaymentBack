namespace PaymentBack.Entities
{
    public class PaymentGroupedByDayStatsEntity
    {
        public DateTime Date { get; set; }
        public int PaymentCount { get; set; }

        public decimal PaymentTotal { set; get; }
    }
}
