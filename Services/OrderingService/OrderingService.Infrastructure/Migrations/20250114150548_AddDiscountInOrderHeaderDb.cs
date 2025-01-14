using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.OrderingService.OrderingService.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountInOrderHeaderDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BillingAddress_AddressLine",
                table: "OrderHeaders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "OrderHeaders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "OrderHeaders");

            migrationBuilder.AlterColumn<string>(
                name: "BillingAddress_AddressLine",
                table: "OrderHeaders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
