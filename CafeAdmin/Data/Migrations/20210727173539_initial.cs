using Microsoft.EntityFrameworkCore.Migrations;

namespace CafeAdmin.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DS = table.Column<string>(type: "varchar(250)", nullable: false),
                    Summ = table.Column<decimal>(type: "TEXT", precision: 10, scale: 4, nullable: false, comment: "Це тестова сумма"),
                    TestLenght = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTables", x => new { x.Name, x.Id });
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
                name: "IX_UserAccesLevels_UserId",
                table: "UserAccesLevels",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientTables");

            migrationBuilder.DropTable(
                name: "UserAccesLevels");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
