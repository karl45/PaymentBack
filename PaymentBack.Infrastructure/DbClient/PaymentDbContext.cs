using Microsoft.EntityFrameworkCore;
using PaymentBack.Domain.Entities;
using PaymentBack.Domain.Enums;


namespace PaymentBack.Infrastructure.DbClient
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> contextOptions) : base(contextOptions)
        {
        }

        public DbSet<PaymentEntity> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .HasPostgresEnum<Status>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentDbContext).Assembly);
        }
    }
}
