using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PaymentBack.Domain.Enums;

#nullable disable

namespace PaymentBack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitPaymentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "payments",
               columns: table => new
               {
                   Id = table.Column<long>(type: "bigint", nullable: false)
                       .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                   WalletNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                   Account = table.Column<long>(type: "bigint", nullable: false),
                   Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                   Phone = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: true),
                   Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                   Currency = table.Column<string>(type: "character(3)", fixedLength: true, maxLength: 3, nullable: false),
                   Comment = table.Column<string>(type: "text", nullable: true),
                   CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                   Status = table.Column<Status>(type: "status", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_payments", x => x.Id);
               });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payments");
        }
    }
}
