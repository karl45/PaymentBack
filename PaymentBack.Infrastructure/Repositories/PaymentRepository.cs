using PaymentBack.Entities;
using Microsoft.EntityFrameworkCore;
using PaymentBack.Infrastructure.DbClient;
using PaymentBack.Domain.Entities;
using PaymentBack.Domain.Enums;

namespace PaymentBack.Infrastructure.Repositories
{

    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentDbContext _dbContext;

        public PaymentRepository(PaymentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaymentEntity> CreatePaymentAsync(PaymentEntity paymentEntity, CancellationToken token)
        {
            _dbContext.Add(paymentEntity);
            await _dbContext.SaveChangesAsync(token);
            return paymentEntity;
        }

        public async Task<List<PaymentEntity>> GetAllPayments(int pageSize,CancellationToken token, int prevId = 0, int lastId = 0)
        {
            IQueryable<PaymentEntity> ordered_payments = _dbContext.Payments;
            
            if(prevId != 0)
                ordered_payments = ordered_payments.Where(x => x.Id > prevId).Take(pageSize).OrderByDescending(x => x.Id);
            else if(lastId != 0)
                ordered_payments = ordered_payments.Where(x => x.Id < lastId).OrderByDescending(x => x.Id).Take(pageSize);
            else
                ordered_payments = ordered_payments.OrderByDescending(x => x.Id).Take(pageSize);

            return await ordered_payments.ToListAsync(token);
        }

        public async Task<PaymentCommonStatsEntity> GetStatsAsync(CancellationToken token)
        {
            var stats = await _dbContext.Payments
                .Where(p => p.Status == Status.Created)
                .GroupBy(p => 1)
                .Select(g => new PaymentCommonStatsEntity
                {
                    PaymentCount = g.Count(),
                    PaymentTotal = g.Sum(p => p.Amount)
                })
                .FirstOrDefaultAsync(token);
            
            return stats ?? new PaymentCommonStatsEntity { PaymentCount = 0, PaymentTotal = 0 };
        }

        public async Task<List<PaymentGroupedByDayStatsEntity>> GetPayStatsByDay(int pageSize, CancellationToken token, DateTime? prevDate = null, DateTime? lastDate = null)
        {
            var paymentStats = _dbContext.Payments
                .Where(p => p.Status == Status.Created)
                .GroupBy(p => p.CreatedAt.Date)
                .Select(g => new PaymentGroupedByDayStatsEntity
                {
                    Date = g.Key,
                    PaymentCount = g.Count(),
                    PaymentTotal = g.Sum(p => p.Amount)
                });


            if (prevDate.HasValue)
                paymentStats = paymentStats.Where(x => x.Date > prevDate.Value).OrderBy(x => x.Date).Take(pageSize).OrderByDescending(x => x.Date);
            else if (lastDate.HasValue)
                paymentStats = paymentStats.Where(x => x.Date < lastDate.Value).OrderByDescending(x => x.Date).Take(pageSize);
            else
                paymentStats = paymentStats.OrderByDescending(x => x.Date).Take(pageSize);

            return await paymentStats
                .ToListAsync(token);
        }
    }
}
