using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editinCcategoryName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "BlogCategories",
                newName: "name_en");

            migrationBuilder.AddColumn<string>(
                name: "name_ar",
                table: "BlogCategories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name_it",
                table: "BlogCategories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name_ru",
                table: "BlogCategories",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name_ar",
                table: "BlogCategories");

            migrationBuilder.DropColumn(
                name: "name_it",
                table: "BlogCategories");

            migrationBuilder.DropColumn(
                name: "name_ru",
                table: "BlogCategories");

            migrationBuilder.RenameColumn(
                name: "name_en",
                table: "BlogCategories",
                newName: "Name");
        }
    }
}
