using Microsoft.EntityFrameworkCore.Migrations;

namespace TP3_Procedures.Migrations
{
    public partial class CreateFriendSelectAllProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[SelectAllFriends]
                AS
                    select * from friend
                RETURN 0
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP PROCEDURE [dbo].[SelectAllFriends]
            ");
        }
    }
}
