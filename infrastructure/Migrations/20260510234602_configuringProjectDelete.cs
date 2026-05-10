using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class configuringProjectDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoiceOfCustomers_Projects_ProjectId",
                table: "VoiceOfCustomers");

            migrationBuilder.AddForeignKey(
                name: "FK_VoiceOfCustomers_Projects_ProjectId",
                table: "VoiceOfCustomers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoiceOfCustomers_Projects_ProjectId",
                table: "VoiceOfCustomers");

            migrationBuilder.AddForeignKey(
                name: "FK_VoiceOfCustomers_Projects_ProjectId",
                table: "VoiceOfCustomers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
