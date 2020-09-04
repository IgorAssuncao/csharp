using Microsoft.EntityFrameworkCore.Migrations;

namespace TP3_Procedures.Migrations
{
    public partial class DeleteFriendUpdateProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[DeleteFriend]
                    @Id uniqueidentifier
                AS
                    delete from friend 
                    where Id = @Id
                RETURN 0
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE [dbo].[DeleteFriend]");
        }
    }
}
