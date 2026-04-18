using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentBack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InsertManyTestPayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO payments (\"WalletNumber\",\"Account\",\"Email\",\"Phone\",\"Amount\",\"Currency\",\"Comment\", \"Status\",\"CreatedAt\") " +
                 "SELECT     'wallet_' || floor(random() * 1000 + 1),   " +
                 "floor(random() * 1000 + 1)," +
                 "'user' || i || '@mail.ru'," +
                 "'+71000000000' + floor(random() * 10000), " +
                 "(random() * 5000 + 10)::numeric(10,2)," +
                 "'USD'," +
                 "'Test transaction ' || i," +
                 "(CASE WHEN random() > 0.5 THEN 'created' ELSE 'rejected' END)::status," +
                 "(NOW() - interval '3 days') + (i * (interval '3 days' / 100)) FROM generate_series(1, 100) AS s(i);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
