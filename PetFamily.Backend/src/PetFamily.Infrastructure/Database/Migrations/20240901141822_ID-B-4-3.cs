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
            migrationBuilder.AlterColumn<string>(
                name: "residential_address",
                table: "pets",
                type: "character varying(370)",
                maxLength: 370,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "residential_address",
                table: "pets",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(370)",
                oldMaxLength: 370);
        }
    }
}
