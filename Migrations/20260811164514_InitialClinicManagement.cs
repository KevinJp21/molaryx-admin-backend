using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class InitialClinicManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "appointment_statuses",
                columns: table => new
                {
                    id_appointment_status = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointment_statuses", x => x.id_appointment_status);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    id_patient = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_tenant = table.Column<long>(type: "bigint", nullable: false),
                    id_identification_type = table.Column<short>(type: "smallint", nullable: false),
                    identification_number = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    second_name = table.Column<string>(type: "text", nullable: true),
                    first_surname = table.Column<string>(type: "text", nullable: false),
                    second_surname = table.Column<string>(type: "text", nullable: true),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.id_patient);
                    table.UniqueConstraint("AK_patients_id_tenant_id_patient", x => new { x.id_tenant, x.id_patient });
                    table.ForeignKey(
                        name: "FK_patients_identification_types_id_identification_type",
                        column: x => x.id_identification_type,
                        principalTable: "identification_types",
                        principalColumn: "id_identification_type",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_patients_tenants_id_tenant",
                        column: x => x.id_tenant,
                        principalTable: "tenants",
                        principalColumn: "id_tenant",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "professionals",
                columns: table => new
                {
                    id_professional = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_tenant = table.Column<long>(type: "bigint", nullable: false),
                    id_user = table.Column<long>(type: "bigint", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professionals", x => x.id_professional);
                    table.UniqueConstraint("AK_professionals_id_tenant_id_professional", x => new { x.id_tenant, x.id_professional });
                    table.ForeignKey(
                        name: "FK_professionals_tenants_id_tenant",
                        column: x => x.id_tenant,
                        principalTable: "tenants",
                        principalColumn: "id_tenant",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_professionals_users_id_user",
                        column: x => x.id_user,
                        principalTable: "users",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    id_service = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_tenant = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    duration_minutes = table.Column<short>(type: "smallint", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.id_service);
                    table.UniqueConstraint("AK_services_id_tenant_id_service", x => new { x.id_tenant, x.id_service });
                    table.ForeignKey(
                        name: "FK_services_tenants_id_tenant",
                        column: x => x.id_tenant,
                        principalTable: "tenants",
                        principalColumn: "id_tenant",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "professional_schedules",
                columns: table => new
                {
                    id_professional_schedule = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_tenant = table.Column<long>(type: "bigint", nullable: false),
                    id_professional = table.Column<long>(type: "bigint", nullable: false),
                    day_of_week = table.Column<short>(type: "smallint", nullable: false),
                    start_time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    end_time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    id_appointment = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_tenant = table.Column<long>(type: "bigint", nullable: false),
                    id_patient = table.Column<long>(type: "bigint", nullable: false),
                    id_professional = table.Column<long>(type: "bigint", nullable: false),
                    id_service = table.Column<long>(type: "bigint", nullable: false),
                    id_appointment_status = table.Column<short>(type: "smallint", nullable: false),
                    start_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    end_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointments", x => x.id_appointment);
                    table.ForeignKey(
                        name: "FK_appointments_appointment_statuses_id_appointment_status",
                        column: x => x.id_appointment_status,
                        principalTable: "appointment_statuses",
                        principalColumn: "id_appointment_status",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_appointments_patients_id_tenant_id_patient",
                        columns: x => new { x.id_tenant, x.id_patient },
                        principalTable: "patients",
                        principalColumns: new[] { "id_tenant", "id_patient" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_appointments_professionals_id_tenant_id_professional",
                        columns: x => new { x.id_tenant, x.id_professional },
                        principalTable: "professionals",
                        principalColumns: new[] { "id_tenant", "id_professional" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_appointments_services_id_tenant_id_service",
                        columns: x => new { x.id_tenant, x.id_service },
                        principalTable: "services",
                        principalColumns: new[] { "id_tenant", "id_service" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_appointments_tenants_id_tenant",
                        column: x => x.id_tenant,
                        principalTable: "tenants",
                        principalColumn: "id_tenant",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "appointment_statuses",
                columns: new[] { "id_appointment_status", "code", "created_at", "name", "updated_at" },
                values: new object[,]
                {
                    { (short)1, "PENDING", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Pendiente", null },
                    { (short)2, "CONFIRMED", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Confirmado", null },
                    { (short)3, "IN_PROGRESS", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "En progreso", null },
                    { (short)4, "COMPLETED", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Completado", null },
                    { (short)5, "CANCELLED", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Cancelado", null },
                    { (short)6, "NO_SHOW", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "No se presentó", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointments_id_appointment_status",
                table: "appointments",
                column: "id_appointment_status");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_id_tenant_id_patient",
                table: "appointments",
                columns: new[] { "id_tenant", "id_patient" });

            migrationBuilder.CreateIndex(
                name: "IX_appointments_id_tenant_id_professional",
                table: "appointments",
                columns: new[] { "id_tenant", "id_professional" });

            migrationBuilder.CreateIndex(
                name: "IX_appointments_id_tenant_id_service",
                table: "appointments",
                columns: new[] { "id_tenant", "id_service" });

            migrationBuilder.CreateIndex(
                name: "IX_patients_id_identification_type",
                table: "patients",
                column: "id_identification_type");

            migrationBuilder.CreateIndex(
                name: "IX_patients_id_tenant_id_identification_type_identification_nu~",
                table: "patients",
                columns: new[] { "id_tenant", "id_identification_type", "identification_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_patients_id_tenant_id_patient",
                table: "patients",
                columns: new[] { "id_tenant", "id_patient" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_professional_schedules_id_tenant_id_professional_day_of_wee~",
                table: "professional_schedules",
                columns: new[] { "id_tenant", "id_professional", "day_of_week", "start_time", "end_time" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_professionals_id_tenant_id_professional",
                table: "professionals",
                columns: new[] { "id_tenant", "id_professional" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_professionals_id_user",
                table: "professionals",
                column: "id_user",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_services_id_tenant_id_service",
                table: "services",
                columns: new[] { "id_tenant", "id_service" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "professional_schedules");

            migrationBuilder.DropTable(
                name: "appointment_statuses");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "services");

            migrationBuilder.DropTable(
                name: "professionals");
        }
    }
}
