using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "group",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    group_name = table.Column<string>(type: "varchar(200)", nullable: false),
                    limit_member = table.Column<int>(type: "integer", nullable: true),
                    note = table.Column<string>(type: "varchar(200)", nullable: true),
                    created_user = table.Column<string>(type: "varchar(100)", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_user = table.Column<string>(type: "varchar(100)", nullable: true),
                    last_updated = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "group_detail",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    group_id = table.Column<int>(type: "integer", nullable: false),
                    created_user = table.Column<string>(type: "varchar(100)", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_user = table.Column<string>(type: "varchar(100)", nullable: true),
                    last_updated = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_detail", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    password = table.Column<string>(type: "varchar(200)", nullable: false),
                    full_name = table.Column<string>(type: "varchar(200)", nullable: false),
                    phone = table.Column<string>(type: "varchar(20)", nullable: false),
                    avatar = table.Column<string>(type: "varchar(200)", nullable: false),
                    role = table.Column<string>(type: "varchar(20)", nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "timestamp", nullable: true),
                    created_user = table.Column<string>(type: "varchar(100)", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_user = table.Column<string>(type: "varchar(100)", nullable: true),
                    last_updated = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "group");

            migrationBuilder.DropTable(
                name: "group_detail");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
