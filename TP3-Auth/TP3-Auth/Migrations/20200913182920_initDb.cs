using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TP3_Auth.Migrations
{
    public partial class initDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Friend",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true, type: "nvarchar(100)", maxLength: 100),
                    Password = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_Friend", x => x.Id);
                        table.UniqueConstraint(
                            name: "UNIQUE_EMAIL",
                            columns: table => new { table.Email }
                        );
                    }
                );

            //migrationBuilder.AddUniqueConstraint(
            //    name: "UNIQUE_EMAIL",
            //    table: "Friend",
            //    column: "Email",
            //    schema: "dbo"
            //);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friend");
        }
    }
}
