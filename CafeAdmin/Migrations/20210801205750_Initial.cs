using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CafeAdmin.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    IsFood = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    WaiterId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientTables_Users_WaiterId",
                        column: x => x.WaiterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAccesLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    AccessLevel = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccesLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccesLevels_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TableId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsPaid = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_ClientTables_TableId",
                        column: x => x.TableId,
                        principalTable: "ClientTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdersGoods",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "INTEGER", nullable: false),
                    GoodId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsServed = table.Column<bool>(type: "INTEGER", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatorId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsDone = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersGoods", x => new { x.GoodId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_OrdersGoods_Goods_GoodId",
                        column: x => x.GoodId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersGoods_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersGoods_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[] { 1, "Admin", "12345" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[] { 2, "Ivan Gardin", "12345" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[] { 3, "Petro Stepanov", "12345" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[] { 4, "Mirko Shuher", "12345" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[] { 5, "Peter Hugert", "12345" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[] { 6, "Ruzhena Stefanic", "12345" });

            migrationBuilder.InsertData(
                table: "UserAccesLevels",
                columns: new[] { "Id", "AccessLevel", "UserId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "UserAccesLevels",
                columns: new[] { "Id", "AccessLevel", "UserId" },
                values: new object[] { 2, 2, 2 });

            migrationBuilder.InsertData(
                table: "UserAccesLevels",
                columns: new[] { "Id", "AccessLevel", "UserId" },
                values: new object[] { 3, 2, 3 });

            migrationBuilder.InsertData(
                table: "UserAccesLevels",
                columns: new[] { "Id", "AccessLevel", "UserId" },
                values: new object[] { 4, 2, 4 });

            migrationBuilder.InsertData(
                table: "UserAccesLevels",
                columns: new[] { "Id", "AccessLevel", "UserId" },
                values: new object[] { 5, 3, 5 });

            migrationBuilder.InsertData(
                table: "UserAccesLevels",
                columns: new[] { "Id", "AccessLevel", "UserId" },
                values: new object[] { 6, 3, 6 });

            migrationBuilder.InsertData(
                table: "UserAccesLevels",
                columns: new[] { "Id", "AccessLevel", "UserId" },
                values: new object[] { 7, 2, 6 });

            migrationBuilder.CreateIndex(
                name: "IX_ClientTables_WaiterId",
                table: "ClientTables",
                column: "WaiterId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TableId",
                table: "Orders",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersGoods_CreatorId",
                table: "OrdersGoods",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersGoods_OrdersId",
                table: "OrdersGoods",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccesLevels_UserId",
                table: "UserAccesLevels",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersGoods");

            migrationBuilder.DropTable(
                name: "UserAccesLevels");

            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ClientTables");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
