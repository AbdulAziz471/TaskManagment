using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQueryRelation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Queries");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Queries");

            migrationBuilder.AddColumn<int>(
                name: "AssignedUserId",
                table: "Queries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Queries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PriorityId",
                table: "Queries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Queries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Queries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Queries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Queries_AssignedUserId",
                table: "Queries",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Queries_CreatedById",
                table: "Queries",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Queries_PriorityId",
                table: "Queries",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Queries_ProjectId",
                table: "Queries",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Queries_StatusId",
                table: "Queries",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Queries_UserId",
                table: "Queries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Queries_Priorities_PriorityId",
                table: "Queries",
                column: "PriorityId",
                principalTable: "Priorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Queries_Projects_ProjectId",
                table: "Queries",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Queries_Statuses_StatusId",
                table: "Queries",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Queries_Users_AssignedUserId",
                table: "Queries",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Queries_Users_CreatedById",
                table: "Queries",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Queries_Users_UserId",
                table: "Queries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Queries_Priorities_PriorityId",
                table: "Queries");

            migrationBuilder.DropForeignKey(
                name: "FK_Queries_Projects_ProjectId",
                table: "Queries");

            migrationBuilder.DropForeignKey(
                name: "FK_Queries_Statuses_StatusId",
                table: "Queries");

            migrationBuilder.DropForeignKey(
                name: "FK_Queries_Users_AssignedUserId",
                table: "Queries");

            migrationBuilder.DropForeignKey(
                name: "FK_Queries_Users_CreatedById",
                table: "Queries");

            migrationBuilder.DropForeignKey(
                name: "FK_Queries_Users_UserId",
                table: "Queries");

            migrationBuilder.DropIndex(
                name: "IX_Queries_AssignedUserId",
                table: "Queries");

            migrationBuilder.DropIndex(
                name: "IX_Queries_CreatedById",
                table: "Queries");

            migrationBuilder.DropIndex(
                name: "IX_Queries_PriorityId",
                table: "Queries");

            migrationBuilder.DropIndex(
                name: "IX_Queries_ProjectId",
                table: "Queries");

            migrationBuilder.DropIndex(
                name: "IX_Queries_StatusId",
                table: "Queries");

            migrationBuilder.DropIndex(
                name: "IX_Queries_UserId",
                table: "Queries");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "Queries");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Queries");

            migrationBuilder.DropColumn(
                name: "PriorityId",
                table: "Queries");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Queries");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Queries");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Queries");

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "Queries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Queries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
