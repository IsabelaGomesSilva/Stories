using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stories.Data.Migrations
{
    /// <inheritdoc />
    public partial class Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Vote_StoryId",
                table: "Vote",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_UserId",
                table: "Vote",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Story_DepartmentId",
                table: "Story",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Story",
                table: "Story",
                column: "DepartmentId",
                principalTable: "Departament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Story_Vote",
                table: "Vote",
                column: "StoryId",
                principalTable: "Story",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Vote",
                table: "Vote",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Story",
                table: "Story");

            migrationBuilder.DropForeignKey(
                name: "FK_Story_Vote",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Vote",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_StoryId",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_UserId",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Story_DepartmentId",
                table: "Story");
        }
    }
}
