using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreWebApp_DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenamePurchasePK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Purchase",
                newName: "purchaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "purchaseId",
                table: "Purchase",
                newName: "Id");
        }
    }
}
