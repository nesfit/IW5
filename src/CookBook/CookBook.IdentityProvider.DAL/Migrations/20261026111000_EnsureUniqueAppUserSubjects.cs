using CookBook.IdentityProvider.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookBook.IdentityProvider.DAL.Migrations;

[DbContext(typeof(IdentityProviderDbContext))]
[Migration("20261026111000_EnsureUniqueAppUserSubjects")]
public partial class EnsureUniqueAppUserSubjects : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("""
            UPDATE AspNetUsers
            SET Subject = CONCAT(N'legacy-long-', CONVERT(nvarchar(36), NEWID()))
            WHERE LEN(Subject) > 450;

            WITH DuplicateSubjects AS (
                SELECT Id, ROW_NUMBER() OVER (PARTITION BY Subject ORDER BY Id) AS SubjectNumber
                FROM AspNetUsers
            )
            UPDATE AspNetUsers
            SET Subject = CONCAT(N'legacy-duplicate-', CONVERT(nvarchar(36), NEWID()))
            FROM AspNetUsers
            INNER JOIN DuplicateSubjects ON AspNetUsers.Id = DuplicateSubjects.Id
            WHERE DuplicateSubjects.SubjectNumber > 1;
            """);

        migrationBuilder.AlterColumn<string>(
            name: "Subject",
            table: "AspNetUsers",
            type: "nvarchar(450)",
            maxLength: 450,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUsers_Subject",
            table: "AspNetUsers",
            column: "Subject",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_AspNetUsers_Subject",
            table: "AspNetUsers");

        migrationBuilder.AlterColumn<string>(
            name: "Subject",
            table: "AspNetUsers",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)",
            oldMaxLength: 450);
    }
}
