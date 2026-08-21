using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class deleteProfesionalSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "professional_schedules");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "professional_schedules",
                columns: table => new
                {
                    id_professional_schedule = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_tenant = table.Column<long>(type: "bigint", nullable: false),
                    id_professional = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    day_of_week = table.Column<short>(type: "smallint", nullable: false),
                    end_time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    start_time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professional_schedules", x => x.id_professional_schedule);
                    table.ForeignKey(
                        name: "FK_professional_schedules_professionals_id_tenant_id_professio~",
                        columns: x => new { x.id_tenant, x.id_professional },
                        principalTable: "professionals",
                        principalColumns: new[] { "id_tenant", "id_professional" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_professional_schedules_tenants_id_tenant",
                        column: x => x.id_tenant,
                        principalTable: "tenants",
                        principalColumn: "id_tenant",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_professional_schedules_id_tenant_id_professional_day_of_wee~",
                table: "professional_schedules",
                columns: new[] { "id_tenant", "id_professional", "day_of_week", "start_time", "end_time" },
                unique: true);
        }
    }
}
