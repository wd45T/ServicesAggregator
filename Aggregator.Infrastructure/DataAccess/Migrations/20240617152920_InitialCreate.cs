using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aggregator.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "companies");

            migrationBuilder.EnsureSchema(
                name: "services");

            migrationBuilder.CreateTable(
                name: "companies",
                schema: "companies",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    logo_url = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_companies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "service_types",
                schema: "services",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_service_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "services",
                schema: "services",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    data = table.Column<string>(type: "jsonb", nullable: false),
                    company_id = table.Column<long>(type: "bigint", nullable: false),
                    service_type_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_services", x => x.id);
                    table.ForeignKey(
                        name: "fk_services_companies_company_id",
                        column: x => x.company_id,
                        principalSchema: "companies",
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_services_service_types_service_type_id",
                        column: x => x.service_type_id,
                        principalSchema: "services",
                        principalTable: "service_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "companies",
                table: "companies",
                columns: new[] { "id", "created_at", "deleted_at", "description", "logo_url", "name", "updated_at" },
                values: new object[,]
                {
                    { 1L, new DateTimeOffset(new DateTime(2024, 6, 17, 15, 29, 19, 886, DateTimeKind.Unspecified).AddTicks(2674), new TimeSpan(0, 0, 0, 0, 0)), null, "Let's clean up your shit", "https://comenian.org/wp-content/uploads/2023/04/istockphoto-1340208950-612x612-1.jpeg", "House Cleaning", null },
                    { 2L, new DateTimeOffset(new DateTime(2024, 6, 17, 15, 29, 19, 886, DateTimeKind.Unspecified).AddTicks(2676), new TimeSpan(0, 0, 0, 0, 0)), null, "Enjoy our pizza", null, "Cooking pizza", null },
                    { 3L, new DateTimeOffset(new DateTime(2024, 6, 17, 15, 29, 19, 886, DateTimeKind.Unspecified).AddTicks(2677), new TimeSpan(0, 0, 0, 0, 0)), null, "Our hands are not for boredom", "https://media.istockphoto.com/id/1463132842/vector/wrench-in-hand-screwdriver-brush-repair-and-service-sign.jpg?s=612x612&w=0&k=20&c=RBWR7k6jh09E9UDXOqviT9hAaex4qmrqX-6gYPnEGbk=", "Handyman", null }
                });

            migrationBuilder.InsertData(
                schema: "services",
                table: "service_types",
                columns: new[] { "id", "created_at", "deleted_at", "name", "updated_at" },
                values: new object[,]
                {
                    { 1L, new DateTimeOffset(new DateTime(2024, 6, 17, 15, 29, 19, 886, DateTimeKind.Unspecified).AddTicks(2641), new TimeSpan(0, 0, 0, 0, 0)), null, "Cleaning", null },
                    { 2L, new DateTimeOffset(new DateTime(2024, 6, 17, 15, 29, 19, 886, DateTimeKind.Unspecified).AddTicks(2650), new TimeSpan(0, 0, 0, 0, 0)), null, "Delivery", null },
                    { 3L, new DateTimeOffset(new DateTime(2024, 6, 17, 15, 29, 19, 886, DateTimeKind.Unspecified).AddTicks(2651), new TimeSpan(0, 0, 0, 0, 0)), null, "Kitchen", null }
                });

            migrationBuilder.InsertData(
                schema: "services",
                table: "services",
                columns: new[] { "id", "company_id", "created_at", "data", "deleted_at", "description", "name", "service_type_id", "updated_at" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTimeOffset(new DateTime(2024, 6, 17, 15, 29, 19, 886, DateTimeKind.Unspecified).AddTicks(2694), new TimeSpan(0, 0, 0, 0, 0)), "{}", null, "Let's clean up your problem", "Let's clean", 1L, null },
                    { 2L, 2L, new DateTimeOffset(new DateTime(2024, 6, 17, 15, 29, 19, 886, DateTimeKind.Unspecified).AddTicks(2695), new TimeSpan(0, 0, 0, 0, 0)), "{}", null, null, "Delivery", 2L, null },
                    { 3L, 3L, new DateTimeOffset(new DateTime(2024, 6, 17, 15, 29, 19, 886, DateTimeKind.Unspecified).AddTicks(2696), new TimeSpan(0, 0, 0, 0, 0)), "{}", null, null, "Super Cleaning", 1L, null },
                    { 4L, 3L, new DateTimeOffset(new DateTime(2024, 6, 17, 15, 29, 19, 886, DateTimeKind.Unspecified).AddTicks(2697), new TimeSpan(0, 0, 0, 0, 0)), "{}", null, null, "Super Delivery", 2L, null },
                    { 5L, 3L, new DateTimeOffset(new DateTime(2024, 6, 17, 15, 29, 19, 886, DateTimeKind.Unspecified).AddTicks(2699), new TimeSpan(0, 0, 0, 0, 0)), "{}", null, null, "Super Kitchen", 3L, null }
                });

            migrationBuilder.CreateIndex(
                name: "ix_services_company_id_service_type_id",
                schema: "services",
                table: "services",
                columns: new[] { "company_id", "service_type_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_services_service_type_id",
                schema: "services",
                table: "services",
                column: "service_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "services",
                schema: "services");

            migrationBuilder.DropTable(
                name: "companies",
                schema: "companies");

            migrationBuilder.DropTable(
                name: "service_types",
                schema: "services");
        }
    }
}
