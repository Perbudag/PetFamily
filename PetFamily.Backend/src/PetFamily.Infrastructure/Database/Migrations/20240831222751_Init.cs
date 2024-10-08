﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "volunteers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    years_of_experience = table.Column<int>(type: "integer", nullable: false),
                    pets_found_home_count = table.Column<int>(type: "integer", nullable: false),
                    pets_looking_for_home_count = table.Column<int>(type: "integer", nullable: false),
                    pets_on_treatment_count = table.Column<int>(type: "integer", nullable: false),
                    phone_number = table.Column<string>(type: "char(11)", nullable: false),
                    firstname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    lastname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    patronymic = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    requisites = table.Column<string>(type: "jsonb", nullable: false),
                    social_networks = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    species = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    breed = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    coloration = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    health_information = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    residential_address = table.Column<string>(type: "text", nullable: false),
                    weight = table.Column<float>(type: "real", nullable: false),
                    height = table.Column<float>(type: "real", nullable: false),
                    phone_number = table.Column<string>(type: "char(11)", nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_castrated = table.Column<bool>(type: "boolean", nullable: false),
                    is_vaccinated = table.Column<bool>(type: "boolean", nullable: false),
                    assistance_status = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: true),
                    photos = table.Column<string>(type: "jsonb", nullable: false),
                    requisites = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pets", x => x.id);
                    table.ForeignKey(
                        name: "fk_pets_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_pets_volunteer_id",
                table: "pets",
                column: "volunteer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pets");

            migrationBuilder.DropTable(
                name: "volunteers");
        }
    }
}
