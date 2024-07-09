using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStoreProject.Migrations
{
    /// <inheritdoc />
    public partial class changedTypeOfDevices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Xbox");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "xbox");
        }
    }
}
