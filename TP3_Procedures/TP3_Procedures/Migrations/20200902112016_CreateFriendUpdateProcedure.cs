using Microsoft.EntityFrameworkCore.Migrations;

namespace TP3_Procedures.Migrations
{
    public partial class CreateFriendUpdateProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[UpdateFriend]
                    @Id uniqueidentifier,
                    @Name varchar(max),
                    @Lastname varchar(max),
                    @Email varchar(max),
                    @Birthday datetime2(7)
                AS
                    update friend set
                        [Name] = @Name,
                        [Lastname] = @Lastname,
                        [Email] = @Email,
                        [Birthday] = @Birthday
                    where Id = @Id
                RETURN 0
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE [dbo].[UpdateFriend]");
        }
    }
}
