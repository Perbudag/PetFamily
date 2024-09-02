using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class IDB43 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "years_of_experience",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "health_information",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "pets_looking_for_home_count",
                table: "volunteers",
                newName: "pets_looking_forHome_count");

            migrationBuilder.AlterColumn<int>(
                name: "pets_on_treatment_count",
                table: "volunteers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "pets_found_home_count",
                table: "volunteers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "pets_looking_forHome_count",
                table: "volunteers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "work_experience",
                table: "volunteers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "health_description",
                table: "pets",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "work_experience",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "health_description",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "pets_looking_forHome_count",
                table: "volunteers",
                newName: "pets_looking_for_home_count");

            migrationBuilder.AlterColumn<int>(
                name: "pets_on_treatment_count",
                table: "volunteers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "pets_found_home_count",
                table: "volunteers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "pets_looking_for_home_count",
                table: "volunteers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "years_of_experience",
                table: "volunteers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "health_information",
                table: "pets",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }
    }
}
