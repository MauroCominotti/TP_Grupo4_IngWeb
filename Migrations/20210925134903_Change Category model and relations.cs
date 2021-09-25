using Microsoft.EntityFrameworkCore.Migrations;

namespace RafaelaColabora.Migrations
{
    public partial class ChangeCategorymodelandrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Category_CategoryId1",
                schema: "Identity",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_CategoryId1",
                schema: "Identity",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                schema: "Identity",
                table: "Post");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                schema: "Identity",
                table: "Post",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Identity",
                table: "Category",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Post_CategoryId",
                schema: "Identity",
                table: "Post",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Category_CategoryId",
                schema: "Identity",
                table: "Post",
                column: "CategoryId",
                principalSchema: "Identity",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Category_CategoryId",
                schema: "Identity",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_CategoryId",
                schema: "Identity",
                table: "Post");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryId",
                schema: "Identity",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 450);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                schema: "Identity",
                table: "Post",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Identity",
                table: "Category",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_Post_CategoryId1",
                schema: "Identity",
                table: "Post",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Category_CategoryId1",
                schema: "Identity",
                table: "Post",
                column: "CategoryId1",
                principalSchema: "Identity",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
