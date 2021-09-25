using Microsoft.EntityFrameworkCore.Migrations;

namespace RafaelaColabora.Migrations
{
    public partial class AddStatetoUserPostNewsCommentandClaim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                schema: "Identity",
                table: "Like");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                schema: "Identity",
                table: "Post",
                maxLength: 20,
                nullable: false,
                defaultValue: "Active",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "State",
                schema: "Identity",
                table: "News",
                maxLength: 20,
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                schema: "Identity",
                table: "Comment",
                maxLength: 20,
                nullable: false,
                defaultValue: "Active",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                schema: "Identity",
                table: "Claim",
                maxLength: 20,
                nullable: false,
                defaultValue: "Active",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "State",
                schema: "Identity",
                table: "ApplicationUser",
                maxLength: 20,
                nullable: false,
                defaultValue: "Active");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                schema: "Identity",
                table: "News");

            migrationBuilder.DropColumn(
                name: "State",
                schema: "Identity",
                table: "ApplicationUser");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                schema: "Identity",
                table: "Post",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldDefaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "State",
                schema: "Identity",
                table: "Like",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                schema: "Identity",
                table: "Comment",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldDefaultValue: "Active");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                schema: "Identity",
                table: "Claim",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldDefaultValue: "Active");
        }
    }
}
