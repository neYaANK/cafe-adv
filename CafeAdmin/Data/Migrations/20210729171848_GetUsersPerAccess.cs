using Microsoft.EntityFrameworkCore.Migrations;

namespace CafeAdmin.Data.Migrations
{
    public partial class GetUsersPerAccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createProcSql = @"CREATE OR ALTER PROC GetUsersPerAccess(@accessLevel int) AS 
            SELECT UserAccessLevel.Id,UserAccessLevel.Name FROM UserAccessLevel";
            migrationBuilder.Sql(createProcSql);


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var drop = @"DROP PROC GetUsersPerAccess";
            migrationBuilder.Sql(drop);
        }
    }
}
