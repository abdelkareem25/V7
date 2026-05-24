using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace V7.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class DelivaryMethodDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DelivaryMethods_DelivaryMethodId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "DelivaryMethodId",
                table: "Orders",
                newName: "DeliveryMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_DelivaryMethodId",
                table: "Orders",
                newName: "IX_Orders_DeliveryMethodId");

            migrationBuilder.InsertData(
                table: "DelivaryMethods",
                columns: new[] { "Id", "Cost", "DeliveryTime", "Description", "ShortName" },
                values: new object[,]
                {
                    { 1, 5.00m, "5-7 days", "Standard delivery", "Standard" },
                    { 2, 15.00m, "1-2 days", "Express delivery", "Express" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DelivaryMethods_DeliveryMethodId",
                table: "Orders",
                column: "DeliveryMethodId",
                principalTable: "DelivaryMethods",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DelivaryMethods_DeliveryMethodId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "DelivaryMethods",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DelivaryMethods",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "DeliveryMethodId",
                table: "Orders",
                newName: "DelivaryMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_DeliveryMethodId",
                table: "Orders",
                newName: "IX_Orders_DelivaryMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DelivaryMethods_DelivaryMethodId",
                table: "Orders",
                column: "DelivaryMethodId",
                principalTable: "DelivaryMethods",
                principalColumn: "Id");
        }
    }
}
