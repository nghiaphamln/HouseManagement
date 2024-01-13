using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "group_detail",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "group_detail",
                newName: "group_id");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "group",
                newName: "note");

            migrationBuilder.RenameColumn(
                name: "MemberLimit",
                table: "group",
                newName: "member_limit");

            migrationBuilder.RenameColumn(
                name: "GroupName",
                table: "group",
                newName: "group_name");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "group_detail",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "group_id",
                table: "group_detail",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "group",
                type: "varchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "group_name",
                table: "group",
                type: "varchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "email",
                table: "group_detail",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "group_id",
                table: "group_detail",
                newName: "GroupId");

            migrationBuilder.RenameColumn(
                name: "note",
                table: "group",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "member_limit",
                table: "group",
                newName: "MemberLimit");

            migrationBuilder.RenameColumn(
                name: "group_name",
                table: "group",
                newName: "GroupName");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "group_detail",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<long>(
                name: "GroupId",
                table: "group_detail",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "group",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "group",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)");
        }
    }
}
