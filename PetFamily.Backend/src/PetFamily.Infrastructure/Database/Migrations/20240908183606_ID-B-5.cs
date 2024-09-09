using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class IDB5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pets_found_home_count",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "pets_looking_forHome_count",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "pets_on_treatment_count",
                table: "volunteers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "pets_found_home_count",
                table: "volunteers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "pets_looking_forHome_count",
                table: "volunteers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "pets_on_treatment_count",
                table: "volunteers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
