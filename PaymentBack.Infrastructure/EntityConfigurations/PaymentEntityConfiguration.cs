using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentBack.Domain.Entities;

namespace PaymentBack.Infrastructure.EntityConfigurations
{
    public class PaymentEntityConfiguration : IEntityTypeConfiguration<PaymentEntity>
    {
        public void Configure(EntityTypeBuilder<PaymentEntity> builder)
        {

            builder.ToTable("payments");
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.Currency)
                   .IsRequired()
                   .IsFixedLength(true)
                   .HasMaxLength(3);

            builder.Property(x => x.Amount)
                     .IsRequired()
                     .HasColumnType("numeric(18,2)");

            builder.Property(x => x.WalletNumber)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(x => x.Phone)
                   .HasMaxLength(12);

            builder.Property(x => x.Status)
                .IsRequired();

        }
    }
}
