using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class AddTreatmentAndPaymentManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "id_patient_treatment",
                table: "appointments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_appointments_id_tenant_id_appointment",
                table: "appointments",
                columns: new[] { "id_tenant", "id_appointment" });

            migrationBuilder.CreateTable(
                name: "payment_frequencies",
                columns: table => new
                {
                    id_payment_frequency = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_frequencies", x => x.id_payment_frequency);
                });

            migrationBuilder.CreateTable(
                name: "payment_methods",
                columns: table => new
                {
                    id_payment_method = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_methods", x => x.id_payment_method);
                });

            migrationBuilder.CreateTable(
                name: "treatment_statuses",
                columns: table => new
                {
                    id_treatment_status = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_treatment_statuses", x => x.id_treatment_status);
                });

            migrationBuilder.CreateTable(
                name: "treatments",
                columns: table => new
                {
                    id_treatment = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_tenant = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_treatments", x => x.id_treatment);
                    table.UniqueConstraint("AK_treatments_id_tenant_id_treatment", x => new { x.id_tenant, x.id_treatment });
                    table.ForeignKey(
                        name: "FK_treatments_tenants_id_tenant",
                        column: x => x.id_tenant,
                        principalTable: "tenants",
                        principalColumn: "id_tenant",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "patient_treatments",
                columns: table => new
                {
                    id_patient_treatment = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_tenant = table.Column<long>(type: "bigint", nullable: false),
                    id_patient = table.Column<long>(type: "bigint", nullable: false),
                    id_treatment = table.Column<long>(type: "bigint", nullable: false),
                    agreed_price = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true),
                    id_payment_frequency = table.Column<short>(type: "smallint", nullable: true),
                    periodic_amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true),
                    start_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    end_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_treatment_status = table.Column<short>(type: "smallint", nullable: false),
                    notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patient_treatments", x => x.id_patient_treatment);
                    table.UniqueConstraint("AK_patient_treatments_id_tenant_id_patient_treatment", x => new { x.id_tenant, x.id_patient_treatment });
                    table.ForeignKey(
                        name: "FK_patient_treatments_patients_id_tenant_id_patient",
                        columns: x => new { x.id_tenant, x.id_patient },
                        principalTable: "patients",
                        principalColumns: new[] { "id_tenant", "id_patient" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_patient_treatments_payment_frequencies_id_payment_frequency",
                        column: x => x.id_payment_frequency,
                        principalTable: "payment_frequencies",
                        principalColumn: "id_payment_frequency",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_patient_treatments_tenants_id_tenant",
                        column: x => x.id_tenant,
                        principalTable: "tenants",
                        principalColumn: "id_tenant",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_patient_treatments_treatment_statuses_id_treatment_status",
                        column: x => x.id_treatment_status,
                        principalTable: "treatment_statuses",
                        principalColumn: "id_treatment_status",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_patient_treatments_treatments_id_tenant_id_treatment",
                        columns: x => new { x.id_tenant, x.id_treatment },
                        principalTable: "treatments",
                        principalColumns: new[] { "id_tenant", "id_treatment" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    id_payment = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_tenant = table.Column<long>(type: "bigint", nullable: false),
                    id_patient = table.Column<long>(type: "bigint", nullable: false),
                    id_appointment = table.Column<long>(type: "bigint", nullable: true),
                    id_patient_treatment = table.Column<long>(type: "bigint", nullable: true),
                    amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    paid_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    id_payment_method = table.Column<short>(type: "smallint", nullable: false),
                    notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.id_payment);
                    table.CheckConstraint("ck_payments_appointment_or_treatment", "(\r\n    id_appointment IS NOT NULL\r\n    AND id_patient_treatment IS NULL\r\n)\r\nOR\r\n(\r\n    id_appointment IS NULL\r\n    AND id_patient_treatment IS NOT NULL\r\n)");
                    table.ForeignKey(
                        name: "FK_payments_appointments_id_tenant_id_appointment",
                        columns: x => new { x.id_tenant, x.id_appointment },
                        principalTable: "appointments",
                        principalColumns: new[] { "id_tenant", "id_appointment" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_payments_patient_treatments_id_tenant_id_patient_treatment",
                        columns: x => new { x.id_tenant, x.id_patient_treatment },
                        principalTable: "patient_treatments",
                        principalColumns: new[] { "id_tenant", "id_patient_treatment" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_payments_patients_id_tenant_id_patient",
                        columns: x => new { x.id_tenant, x.id_patient },
                        principalTable: "patients",
                        principalColumns: new[] { "id_tenant", "id_patient" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_payments_payment_methods_id_payment_method",
                        column: x => x.id_payment_method,
                        principalTable: "payment_methods",
                        principalColumn: "id_payment_method",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_payments_tenants_id_tenant",
                        column: x => x.id_tenant,
                        principalTable: "tenants",
                        principalColumn: "id_tenant",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "payment_frequencies",
                columns: new[] { "id_payment_frequency", "created_at", "is_active", "name", "updated_at" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), true, "Pago único", null },
                    { (short)2, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), true, "Semanal", null },
                    { (short)3, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), true, "Quincenal", null },
                    { (short)4, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), true, "Mensual", null }
                });

            migrationBuilder.InsertData(
                table: "payment_methods",
                columns: new[] { "id_payment_method", "created_at", "is_active", "name", "updated_at" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), true, "Efectivo", null },
                    { (short)2, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), true, "Tarjeta", null },
                    { (short)3, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), true, "Transferencia", null },
                    { (short)4, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), true, "Otro", null }
                });

            migrationBuilder.InsertData(
                table: "treatment_statuses",
                columns: new[] { "id_treatment_status", "created_at", "is_active", "name", "updated_at" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), true, "Activo", null },
                    { (short)2, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), true, "Pausado", null },
                    { (short)3, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), true, "Completado", null },
                    { (short)4, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), true, "Cancelado", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointments_id_tenant_id_appointment",
                table: "appointments",
                columns: new[] { "id_tenant", "id_appointment" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_appointments_id_tenant_id_patient_treatment",
                table: "appointments",
                columns: new[] { "id_tenant", "id_patient_treatment" });

            migrationBuilder.CreateIndex(
                name: "IX_patient_treatments_id_payment_frequency",
                table: "patient_treatments",
                column: "id_payment_frequency");

            migrationBuilder.CreateIndex(
                name: "IX_patient_treatments_id_tenant_id_patient",
                table: "patient_treatments",
                columns: new[] { "id_tenant", "id_patient" });

            migrationBuilder.CreateIndex(
                name: "IX_patient_treatments_id_tenant_id_treatment",
                table: "patient_treatments",
                columns: new[] { "id_tenant", "id_treatment" });

            migrationBuilder.CreateIndex(
                name: "IX_patient_treatments_id_treatment_status",
                table: "patient_treatments",
                column: "id_treatment_status");

            migrationBuilder.CreateIndex(
                name: "IX_payments_id_payment_method",
                table: "payments",
                column: "id_payment_method");

            migrationBuilder.CreateIndex(
                name: "IX_payments_id_tenant_id_appointment",
                table: "payments",
                columns: new[] { "id_tenant", "id_appointment" });

            migrationBuilder.CreateIndex(
                name: "IX_payments_id_tenant_id_patient",
                table: "payments",
                columns: new[] { "id_tenant", "id_patient" });

            migrationBuilder.CreateIndex(
                name: "IX_payments_id_tenant_id_patient_treatment",
                table: "payments",
                columns: new[] { "id_tenant", "id_patient_treatment" });

            migrationBuilder.CreateIndex(
                name: "IX_treatments_id_tenant_name",
                table: "treatments",
                columns: new[] { "id_tenant", "name" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_patient_treatments_id_tenant_id_patient_treatm~",
                table: "appointments",
                columns: new[] { "id_tenant", "id_patient_treatment" },
                principalTable: "patient_treatments",
                principalColumns: new[] { "id_tenant", "id_patient_treatment" },
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointments_patient_treatments_id_tenant_id_patient_treatm~",
                table: "appointments");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "patient_treatments");

            migrationBuilder.DropTable(
                name: "payment_methods");

            migrationBuilder.DropTable(
                name: "payment_frequencies");

            migrationBuilder.DropTable(
                name: "treatment_statuses");

            migrationBuilder.DropTable(
                name: "treatments");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_appointments_id_tenant_id_appointment",
                table: "appointments");

            migrationBuilder.DropIndex(
                name: "IX_appointments_id_tenant_id_appointment",
                table: "appointments");

            migrationBuilder.DropIndex(
                name: "IX_appointments_id_tenant_id_patient_treatment",
                table: "appointments");

            migrationBuilder.DropColumn(
                name: "id_patient_treatment",
                table: "appointments");
        }
    }
}
