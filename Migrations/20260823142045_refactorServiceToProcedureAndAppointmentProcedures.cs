using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class refactorServiceToProcedureAndAppointmentProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Soltar FKs que apuntan a services (se recrean después del rename).
            migrationBuilder.DropForeignKey(
                name: "FK_appointments_services_id_tenant_id_service",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_clinical_records_services_id_tenant_id_service",
                table: "clinical_records");

            // 2. Renombrar tabla y columna PK (preserva datos del catálogo).
            migrationBuilder.RenameTable(
                name: "services",
                newName: "procedures");

            migrationBuilder.RenameColumn(
                name: "id_service",
                table: "procedures",
                newName: "id_procedure");

            migrationBuilder.Sql("""
                ALTER TABLE procedures RENAME CONSTRAINT "PK_services" TO "PK_procedures";
                ALTER TABLE procedures RENAME CONSTRAINT "AK_services_id_tenant_id_service" TO "AK_procedures_id_tenant_id_procedure";
                ALTER TABLE procedures RENAME CONSTRAINT "FK_services_tenants_id_tenant" TO "FK_procedures_tenants_id_tenant";
                ALTER INDEX "IX_services_id_tenant_id_service" RENAME TO "IX_procedures_id_tenant_id_procedure";
                ALTER INDEX "IX_services_id_tenant_name" RENAME TO "IX_procedures_id_tenant_name";
                ALTER SEQUENCE IF EXISTS services_id_service_seq RENAME TO procedures_id_procedure_seq;
                """);

            migrationBuilder.AddColumn<decimal>(
                name: "reference_price",
                table: "procedures",
                type: "numeric(12,2)",
                precision: 12,
                scale: 2,
                nullable: true);

            // 3. Crear appointment_procedures (appointments aún tiene id_service/price).
            migrationBuilder.CreateTable(
                name: "appointment_procedures",
                columns: table => new
                {
                    id_appointment_procedure = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_appointment = table.Column<long>(type: "bigint", nullable: false),
                    id_procedure = table.Column<long>(type: "bigint", nullable: false),
                    price = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointment_procedures", x => x.id_appointment_procedure);
                    table.ForeignKey(
                        name: "FK_appointment_procedures_appointments_id_appointment",
                        column: x => x.id_appointment,
                        principalTable: "appointments",
                        principalColumn: "id_appointment",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_appointment_procedures_procedures_id_procedure",
                        column: x => x.id_procedure,
                        principalTable: "procedures",
                        principalColumn: "id_procedure",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointment_procedures_id_appointment_id_procedure",
                table: "appointment_procedures",
                columns: new[] { "id_appointment", "id_procedure" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_appointment_procedures_id_procedure",
                table: "appointment_procedures",
                column: "id_procedure");

            // 4. Backfill: 1 AppointmentProcedure por cita existente.
            migrationBuilder.Sql("""
                INSERT INTO appointment_procedures
                (
                    id_appointment,
                    id_procedure,
                    price,
                    notes,
                    created_at,
                    updated_at
                )
                SELECT
                    a.id_appointment,
                    a.id_service,
                    COALESCE(a.price, 0),
                    NULL,
                    COALESCE(a.created_at, NOW() AT TIME ZONE 'UTC'),
                    NULL
                FROM appointments a
                WHERE a.id_service IS NOT NULL;
                """);

            // 5. Clinical records: id_service -> id_procedure.
            migrationBuilder.RenameColumn(
                name: "id_service",
                table: "clinical_records",
                newName: "id_procedure");

            migrationBuilder.RenameIndex(
                name: "IX_clinical_records_id_tenant_id_service",
                table: "clinical_records",
                newName: "IX_clinical_records_id_tenant_id_procedure");

            migrationBuilder.AddForeignKey(
                name: "FK_clinical_records_procedures_id_tenant_id_procedure",
                table: "clinical_records",
                columns: new[] { "id_tenant", "id_procedure" },
                principalTable: "procedures",
                principalColumns: new[] { "id_tenant", "id_procedure" },
                onDelete: ReferentialAction.Restrict);

            // 6. Quitar constraint y columnas antiguas de appointments.
            migrationBuilder.DropCheckConstraint(
                name: "ck_appointments_treatment_or_price",
                table: "appointments");

            migrationBuilder.DropIndex(
                name: "IX_appointments_id_tenant_id_service",
                table: "appointments");

            migrationBuilder.DropColumn(
                name: "id_service",
                table: "appointments");

            migrationBuilder.DropColumn(
                name: "price",
                table: "appointments");

            // 7. Permisos/módulo: TMP primero por unique de code.
            migrationBuilder.Sql("""
                UPDATE modules
                SET code = 'TMP_MOD_4'
                WHERE id_module = 4;

                UPDATE permissions
                SET code = 'TMP_PERM_' || id_permission::text
                WHERE id_permission IN (8, 9, 10, 11);
                """);

            migrationBuilder.UpdateData(
                table: "modules",
                keyColumn: "id_module",
                keyValue: (short)4,
                column: "code",
                value: "PROCEDURES");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)8,
                column: "code",
                value: "GET_PROCEDURES");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)9,
                column: "code",
                value: "CREATE_PROCEDURE");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)10,
                column: "code",
                value: "UPDATE_PROCEDURE");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)11,
                column: "code",
                value: "DELETE_PROCEDURE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Abortar si ya hay citas con múltiples procedimientos (no reversible sin pérdida).
            migrationBuilder.Sql("""
                DO $$
                BEGIN
                    IF EXISTS (
                        SELECT 1
                        FROM appointment_procedures
                        GROUP BY id_appointment
                        HAVING COUNT(*) > 1
                    ) THEN
                        RAISE EXCEPTION
                            'No se puede revertir refactorServiceToProcedureAndAppointmentProcedures: existen citas con múltiples procedimientos.';
                    END IF;
                END $$;
                """);

            migrationBuilder.Sql("""
                UPDATE modules
                SET code = 'TMP_MOD_4'
                WHERE id_module = 4;

                UPDATE permissions
                SET code = 'TMP_PERM_' || id_permission::text
                WHERE id_permission IN (8, 9, 10, 11);
                """);

            migrationBuilder.UpdateData(
                table: "modules",
                keyColumn: "id_module",
                keyValue: (short)4,
                column: "code",
                value: "SERVICES");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)8,
                column: "code",
                value: "GET_SERVICES");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)9,
                column: "code",
                value: "CREATE_SERVICE");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)10,
                column: "code",
                value: "UPDATE_SERVICE");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)11,
                column: "code",
                value: "DELETE_SERVICE");

            migrationBuilder.AddColumn<long>(
                name: "id_service",
                table: "appointments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "appointments",
                type: "numeric(12,2)",
                precision: 12,
                scale: 2,
                nullable: true);

            // Restaurar id_service/price desde el único AppointmentProcedure.
            migrationBuilder.Sql("""
                UPDATE appointments a
                SET
                    id_service = ap.id_procedure,
                    price = CASE
                        WHEN a.id_patient_treatment IS NOT NULL THEN NULL
                        WHEN ap.price = 0 THEN NULL
                        ELSE ap.price
                    END
                FROM appointment_procedures ap
                WHERE ap.id_appointment = a.id_appointment;
                """);

            migrationBuilder.DropForeignKey(
                name: "FK_clinical_records_procedures_id_tenant_id_procedure",
                table: "clinical_records");

            migrationBuilder.DropTable(
                name: "appointment_procedures");

            migrationBuilder.DropColumn(
                name: "reference_price",
                table: "procedures");

            migrationBuilder.RenameColumn(
                name: "id_procedure",
                table: "clinical_records",
                newName: "id_service");

            migrationBuilder.RenameIndex(
                name: "IX_clinical_records_id_tenant_id_procedure",
                table: "clinical_records",
                newName: "IX_clinical_records_id_tenant_id_service");

            migrationBuilder.RenameColumn(
                name: "id_procedure",
                table: "procedures",
                newName: "id_service");

            migrationBuilder.RenameTable(
                name: "procedures",
                newName: "services");

            migrationBuilder.Sql("""
                ALTER TABLE services RENAME CONSTRAINT "PK_procedures" TO "PK_services";
                ALTER TABLE services RENAME CONSTRAINT "AK_procedures_id_tenant_id_procedure" TO "AK_services_id_tenant_id_service";
                ALTER TABLE services RENAME CONSTRAINT "FK_procedures_tenants_id_tenant" TO "FK_services_tenants_id_tenant";
                ALTER INDEX "IX_procedures_id_tenant_id_procedure" RENAME TO "IX_services_id_tenant_id_service";
                ALTER INDEX "IX_procedures_id_tenant_name" RENAME TO "IX_services_id_tenant_name";
                ALTER SEQUENCE IF EXISTS procedures_id_procedure_seq RENAME TO services_id_service_seq;
                """);

            migrationBuilder.CreateIndex(
                name: "IX_appointments_id_tenant_id_service",
                table: "appointments",
                columns: new[] { "id_tenant", "id_service" });

            migrationBuilder.AddCheckConstraint(
                name: "ck_appointments_treatment_or_price",
                table: "appointments",
                sql: """
                    (
                        id_patient_treatment IS NULL
                        OR price IS NULL
                    )
                    """);

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_services_id_tenant_id_service",
                table: "appointments",
                columns: new[] { "id_tenant", "id_service" },
                principalTable: "services",
                principalColumns: new[] { "id_tenant", "id_service" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_clinical_records_services_id_tenant_id_service",
                table: "clinical_records",
                columns: new[] { "id_tenant", "id_service" },
                principalTable: "services",
                principalColumns: new[] { "id_tenant", "id_service" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
