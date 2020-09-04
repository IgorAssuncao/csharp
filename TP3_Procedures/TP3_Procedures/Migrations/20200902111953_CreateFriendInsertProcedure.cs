using Microsoft.EntityFrameworkCore.Migrations;

namespace TP3_Procedures.Migrations
{
    public partial class CreateFriendInsertProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[InsertFriend]
                    @Id uniqueidentifier,
                    @Name varchar(max),
                    @Lastname varchar(max),
                    @Email varchar(max),
                    @Birthday datetime2(7)
                AS
                    insert into friend ([Id], [Name], [Lastname], [Email], [Birthday])
                    values (@Id, @Name, @Lastname, @Email, @Birthday)
                RETURN 0
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TABLE [dbo].[InsertFriend]");
        }
    }
}
