using Microsoft.EntityFrameworkCore.Migrations;

namespace Appweb.Migrations
{
    public partial class _8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Field3",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Field4",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Field5",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Field6",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Field7",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Field8",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Field9",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Type3",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "Type4",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "Type5",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "Type6",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "Type7",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "Type8",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "Type9",
                table: "Collections");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Field3",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field4",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field5",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field6",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field7",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field8",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field9",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type3",
                table: "Collections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type4",
                table: "Collections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type5",
                table: "Collections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type6",
                table: "Collections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type7",
                table: "Collections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type8",
                table: "Collections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type9",
                table: "Collections",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
