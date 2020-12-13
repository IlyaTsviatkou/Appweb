using Microsoft.EntityFrameworkCore.Migrations;

namespace Appweb.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserItem_Items_ItemId",
                table: "UserItem");

            migrationBuilder.DropForeignKey(
                name: "FK_UserItem_AspNetUsers_UserId",
                table: "UserItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserItem",
                table: "UserItem");

            migrationBuilder.RenameTable(
                name: "UserItem",
                newName: "UserItems");

            migrationBuilder.RenameIndex(
                name: "IX_UserItem_ItemId",
                table: "UserItems",
                newName: "IX_UserItems_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserItems",
                table: "UserItems",
                columns: new[] { "UserId", "ItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserItems_Items_ItemId",
                table: "UserItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserItems_AspNetUsers_UserId",
                table: "UserItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserItems_Items_ItemId",
                table: "UserItems");

            migrationBuilder.DropForeignKey(
                name: "FK_UserItems_AspNetUsers_UserId",
                table: "UserItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserItems",
                table: "UserItems");

            migrationBuilder.RenameTable(
                name: "UserItems",
                newName: "UserItem");

            migrationBuilder.RenameIndex(
                name: "IX_UserItems_ItemId",
                table: "UserItem",
                newName: "IX_UserItem_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserItem",
                table: "UserItem",
                columns: new[] { "UserId", "ItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserItem_Items_ItemId",
                table: "UserItem",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserItem_AspNetUsers_UserId",
                table: "UserItem",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
