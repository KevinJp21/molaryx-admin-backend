using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class DropPatientTreatmentTenantAlternateKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointments_patient_treatments_id_tenant_id_patient_treatm~",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_patient_treatments_id_tenant_id_patient_treatment",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_treatments_id_tenant_name",
                table: "treatments");

            migrationBuilder.DropIndex(
                name: "IX_payments_id_tenant_id_patient_treatment",
                table: "payments");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_patient_treatments_id_tenant_id_patient_treatment",
                table: "patient_treatments");

            migrationBuilder.DropIndex(
                name: "IX_appointments_id_tenant_id_patient_treatment",
                table: "appointments");

            migrationBuilder.InsertData(
                table: "modules",
                columns: new[] { "id_module", "code", "created_at", "updated_at" },
                values: new object[] { (short)6, "TREATMENTS", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), null });

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id_permission", "code", "created_at", "id_module", "updated_at" },
                values: new object[] { (short)16, "CREATE_TREATMENT", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), (short)6, null });

            migrationBuilder.InsertData(
                table: "role_permissions",
                columns: new[] { "IdPermission", "IdUserRole" },
                values: new object[] { (short)16, (short)2 });

            migrationBuilder.CreateIndex(
                name: "IX_treatments_id_tenant_id_treatment",
                table: "treatments",
                columns: new[] { "id_tenant", "id_treatment" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_treatments_id_tenant_name",
                table: "treatments",
                columns: new[] { "id_tenant", "name" },
                unique: true,
                filter: "deleted_at IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_payments_id_patient_treatment",
                table: "payments",
                column: "id_patient_treatment");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_id_patient_treatment",
                table: "appointments",
                column: "id_patient_treatment");

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_patient_treatments_id_patient_treatment",
                table: "appointments",
                column: "id_patient_treatment",
                principalTable: "patient_treatments",
                principalColumn: "id_patient_treatment",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_patient_treatments_id_patient_treatment",
                table: "payments",
                column: "id_patient_treatment",
                principalTable: "patient_treatments",
                principalColumn: "id_patient_treatment",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointments_patient_treatments_id_patient_treatment",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_patient_treatments_id_patient_treatment",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_treatments_id_tenant_id_treatment",
                table: "treatments");

            migrationBuilder.DropIndex(
                name: "IX_treatments_id_tenant_name",
                table: "treatments");

            migrationBuilder.DropIndex(
                name: "IX_payments_id_patient_treatment",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_appointments_id_patient_treatment",
                table: "appointments");

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)16, (short)2 });

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)16);

            migrationBuilder.DeleteData(
                table: "modules",
                keyColumn: "id_module",
                keyValue: (short)6);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_patient_treatments_id_tenant_id_patient_treatment",
                table: "patient_treatments",
                columns: new[] { "id_tenant", "id_patient_treatment" });

            migrationBuilder.CreateIndex(
                name: "IX_treatments_id_tenant_name",
                table: "treatments",
                columns: new[] { "id_tenant", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payments_id_tenant_id_patient_treatment",
                table: "payments",
                columns: new[] { "id_tenant", "id_patient_treatment" });

            migrationBuilder.CreateIndex(
                name: "IX_appointments_id_tenant_id_patient_treatment",
                table: "appointments",
                columns: new[] { "id_tenant", "id_patient_treatment" });

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_patient_treatments_id_tenant_id_patient_treatm~",
                table: "appointments",
                columns: new[] { "id_tenant", "id_patient_treatment" },
                principalTable: "patient_treatments",
                principalColumns: new[] { "id_tenant", "id_patient_treatment" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_patient_treatments_id_tenant_id_patient_treatment",
                table: "payments",
                columns: new[] { "id_tenant", "id_patient_treatment" },
                principalTable: "patient_treatments",
                principalColumns: new[] { "id_tenant", "id_patient_treatment" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
