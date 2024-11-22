using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowersShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FlowersShop1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_ExternalId",
                table: "Users",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ExternalId",
                table: "Items",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlowersShop_ExternalId",
                table: "FlowersShop",
                column: "ExternalId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_ExternalId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Items_ExternalId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_FlowersShop_ExternalId",
                table: "FlowersShop");
        }
    }
}
