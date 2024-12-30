using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByUserToIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedTo",
                table: "Issues");
            migrationBuilder.CreateTable(
                name: "IssueUser",
                columns: table => new
                {
                    AssignedIssuesId = table.Column<int>(type: "int", nullable: false),
                    AssignedUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueUser", x => new { x.AssignedIssuesId, x.AssignedUsersId });
                    table.ForeignKey(
                        name: "FK_IssueUser_Issues_AssignedIssuesId",
                        column: x => x.AssignedIssuesId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueUser_Users_AssignedUsersId",
                        column: x => x.AssignedUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_CreatedBy",
                table: "Issues",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IssueUser_AssignedUsersId",
                table: "IssueUser",
                column: "AssignedUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Users_CreatedBy",
                table: "Issues",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Users_CreatedBy",
                table: "Issues");

            migrationBuilder.DropTable(
                name: "IssueUser");

            migrationBuilder.DropIndex(
                name: "IX_Issues_CreatedBy",
                table: "Issues");

            migrationBuilder.AddColumn<string>(
                name: "AssignedTo",
                table: "Issues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
