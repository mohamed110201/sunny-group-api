using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditingMetaFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "MetaTitle",
                table: "Units",
                newName: "meta_title_en");

            migrationBuilder.RenameColumn(
                name: "MetaDescription",
                table: "Units",
                newName: "meta_description_en");

            migrationBuilder.RenameColumn(
                name: "MetaTitle",
                table: "Projects",
                newName: "meta_title_en");

            migrationBuilder.RenameColumn(
                name: "MetaDescription",
                table: "Projects",
                newName: "meta_description_en");

            migrationBuilder.AddColumn<string>(
                name: "meta_description_ar",
                table: "Units",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "meta_description_it",
                table: "Units",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "meta_description_ru",
                table: "Units",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "meta_title_ar",
                table: "Units",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "meta_title_it",
                table: "Units",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "meta_title_ru",
                table: "Units",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "meta_description_ar",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "meta_description_it",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "meta_description_ru",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "meta_title_ar",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "meta_title_it",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "meta_title_ru",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "meta_description_ar",
                table: "Blogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "meta_description_en",
                table: "Blogs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "meta_description_it",
                table: "Blogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "meta_description_ru",
                table: "Blogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "meta_title_ar",
                table: "Blogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "meta_title_en",
                table: "Blogs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "meta_title_it",
                table: "Blogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "meta_title_ru",
                table: "Blogs",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdminId = table.Column<int>(type: "integer", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AdminId",
                table: "RefreshTokens",
                column: "AdminId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropColumn(
                name: "meta_description_ar",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "meta_description_it",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "meta_description_ru",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "meta_title_ar",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "meta_title_it",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "meta_title_ru",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "meta_description_ar",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "meta_description_it",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "meta_description_ru",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "meta_title_ar",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "meta_title_it",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "meta_title_ru",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "meta_description_ar",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "meta_description_en",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "meta_description_it",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "meta_description_ru",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "meta_title_ar",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "meta_title_en",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "meta_title_it",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "meta_title_ru",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "meta_title_en",
                table: "Units",
                newName: "MetaTitle");

            migrationBuilder.RenameColumn(
                name: "meta_description_en",
                table: "Units",
                newName: "MetaDescription");

            migrationBuilder.RenameColumn(
                name: "meta_title_en",
                table: "Projects",
                newName: "MetaTitle");

            migrationBuilder.RenameColumn(
                name: "meta_description_en",
                table: "Projects",
                newName: "MetaDescription");

            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "Blogs",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "Blogs",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
