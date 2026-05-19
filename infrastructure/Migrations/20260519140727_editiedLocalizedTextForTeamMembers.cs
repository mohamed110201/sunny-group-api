using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editiedLocalizedTextForTeamMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Position",
                table: "TeamMembers",
                newName: "position_ru");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TeamMembers",
                newName: "name_en");

            migrationBuilder.RenameColumn(
                name: "Department",
                table: "TeamMembers",
                newName: "position_it");

            migrationBuilder.AddColumn<string>(
                name: "department_ar",
                table: "TeamMembers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "department_en",
                table: "TeamMembers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "department_it",
                table: "TeamMembers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "department_ru",
                table: "TeamMembers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name_ar",
                table: "TeamMembers",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name_it",
                table: "TeamMembers",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name_ru",
                table: "TeamMembers",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "position_ar",
                table: "TeamMembers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "position_en",
                table: "TeamMembers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "department_ar",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "department_en",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "department_it",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "department_ru",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "name_ar",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "name_it",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "name_ru",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "position_ar",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "position_en",
                table: "TeamMembers");

            migrationBuilder.RenameColumn(
                name: "position_ru",
                table: "TeamMembers",
                newName: "Position");

            migrationBuilder.RenameColumn(
                name: "position_it",
                table: "TeamMembers",
                newName: "Department");

            migrationBuilder.RenameColumn(
                name: "name_en",
                table: "TeamMembers",
                newName: "Name");
        }
    }
}
