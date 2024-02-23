using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stories.Data.Migrations
{
    /// <inheritdoc />
    public partial class Voted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Story_Department",
                table: "Story",
                column: "DepartmentId",
                principalTable: "Departament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Story",
                table: "Vote",
                column: "StoryId",
                principalTable: "Story",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_User",
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
                name: "FK_Story_Department",
                table: "Story");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Story",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_User",
                table: "Vote");

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
    }
}
