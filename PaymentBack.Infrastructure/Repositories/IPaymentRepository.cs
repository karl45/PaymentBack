using PaymentBack.Domain.Entities;
using PaymentBack.Entities;

namespace PaymentBack.Infrastructure.Repositories
{
    public interface IPaymentRepository
    {
        Task<PaymentEntity> CreatePaymentAsync(PaymentEntity paymentEntity, CancellationToken token);

        Task<List<PaymentEntity>> GetAllPayments(int pageSize, CancellationToken token, int prevId = 0, int lastId = 0);

        Task<PaymentCommonStatsEntity> GetStatsAsync(CancellationToken token);

        Task<List<PaymentGroupedByDayStatsEntity>> GetPayStatsByDay(int pageSize, CancellationToken token, DateTime? prevDate = null, DateTime? lastDate = null);
    }
}
