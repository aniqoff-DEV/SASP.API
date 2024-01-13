using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SASP.API.Migrations
{
    public partial class ordHistoruUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "OrderHistories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderHistories");
        }
    }
}
