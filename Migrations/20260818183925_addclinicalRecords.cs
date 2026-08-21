using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class addclinicalRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_patient_treatments_id_tenant_id_patient_treatment",
                table: "patient_treatments",
                columns: new[] { "id_tenant", "id_patient_treatment" });

            migrationBuilder.CreateTable(
                name: "clinical_records",
                columns: table => new
                {
                    id_clinical_record = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_tenant = table.Column<long>(type: "bigint", nullable: false),
                    id_patient = table.Column<long>(type: "bigint", nullable: false),
                    id_appointment = table.Column<long>(type: "bigint", nullable: true),
                    id_patient_treatment = table.Column<long>(type: "bigint", nullable: true),
                    id_service = table.Column<long>(type: "bigint", nullable: true),
                    id_created_by_user = table.Column<long>(type: "bigint", nullable: false),
                    recorded_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    reason = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    diagnosis = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    evolution = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clinical_records", x => x.id_clinical_record);
                    table.ForeignKey(
                        name: "FK_clinical_records_appointments_id_tenant_id_appointment",
                        columns: x => new { x.id_tenant, x.id_appointment },
                        principalTable: "appointments",
                        principalColumns: new[] { "id_tenant", "id_appointment" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_clinical_records_patient_treatments_id_tenant_id_patient_tr~",
                        columns: x => new { x.id_tenant, x.id_patient_treatment },
                        principalTable: "patient_treatments",
                        principalColumns: new[] { "id_tenant", "id_patient_treatment" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_clinical_records_patients_id_tenant_id_patient",
                        columns: x => new { x.id_tenant, x.id_patient },
                        principalTable: "patients",
                        principalColumns: new[] { "id_tenant", "id_patient" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_clinical_records_services_id_tenant_id_service",
                        columns: x => new { x.id_tenant, x.id_service },
                        principalTable: "services",
                        principalColumns: new[] { "id_tenant", "id_service" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_clinical_records_tenants_id_tenant",
                        column: x => x.id_tenant,
                        principalTable: "tenants",
                        principalColumn: "id_tenant",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_clinical_records_users_id_created_by_user",
                        column: x => x.id_created_by_user,
                        principalTable: "users",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_clinical_records_id_created_by_user",
                table: "clinical_records",
                column: "id_created_by_user");

            migrationBuilder.CreateIndex(
                name: "IX_clinical_records_id_tenant_id_appointment",
                table: "clinical_records",
                columns: new[] { "id_tenant", "id_appointment" });

            migrationBuilder.CreateIndex(
                name: "IX_clinical_records_id_tenant_id_patient",
                table: "clinical_records",
                columns: new[] { "id_tenant", "id_patient" });

            migrationBuilder.CreateIndex(
                name: "IX_clinical_records_id_tenant_id_patient_treatment",
                table: "clinical_records",
                columns: new[] { "id_tenant", "id_patient_treatment" });

            migrationBuilder.CreateIndex(
                name: "IX_clinical_records_id_tenant_id_service",
                table: "clinical_records",
                columns: new[] { "id_tenant", "id_service" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clinical_records");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_patient_treatments_id_tenant_id_patient_treatment",
                table: "patient_treatments");
        }
    }
}
