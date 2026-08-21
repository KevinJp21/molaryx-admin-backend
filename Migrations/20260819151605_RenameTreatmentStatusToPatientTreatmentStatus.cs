using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class RenameTreatmentStatusToPatientTreatmentStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_patient_treatments_treatment_statuses_id_treatment_status",
                table: "patient_treatments");

            migrationBuilder.DropTable(
                name: "treatment_statuses");

            migrationBuilder.RenameColumn(
                name: "id_treatment_status",
                table: "patient_treatments",
                newName: "id_patient_treatment_status");

            migrationBuilder.RenameIndex(
                name: "IX_patient_treatments_id_treatment_status",
                table: "patient_treatments",
                newName: "IX_patient_treatments_id_patient_treatment_status");

            migrationBuilder.CreateTable(
                name: "patient_treatment_statuses",
                columns: table => new
                {
                    id_patient_treatment_status = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patient_treatment_statuses", x => x.id_patient_treatment_status);
                });

            migrationBuilder.InsertData(
                table: "patient_treatment_statuses",
                columns: new[] { "id_patient_treatment_status", "created_at", "is_active", "name", "updated_at" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), true, "Activo", null },
                    { (short)2, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), true, "Pausado", null },
                    { (short)3, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), true, "Completado", null },
                    { (short)4, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), true, "Cancelado", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_patient_treatments_patient_treatment_statuses_id_patient_tr~",
                table: "patient_treatments",
                column: "id_patient_treatment_status",
                principalTable: "patient_treatment_statuses",
                principalColumn: "id_patient_treatment_status",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_patient_treatments_patient_treatment_statuses_id_patient_tr~",
                table: "patient_treatments");

            migrationBuilder.DropTable(
                name: "patient_treatment_statuses");

            migrationBuilder.RenameColumn(
                name: "id_patient_treatment_status",
                table: "patient_treatments",
                newName: "id_treatment_status");

            migrationBuilder.RenameIndex(
                name: "IX_patient_treatments_id_patient_treatment_status",
                table: "patient_treatments",
                newName: "IX_patient_treatments_id_treatment_status");

            migrationBuilder.CreateTable(
                name: "treatment_statuses",
                columns: table => new
                {
                    id_treatment_status = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_treatment_statuses", x => x.id_treatment_status);
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

            migrationBuilder.AddForeignKey(
                name: "FK_patient_treatments_treatment_statuses_id_treatment_status",
                table: "patient_treatments",
                column: "id_treatment_status",
                principalTable: "treatment_statuses",
                principalColumn: "id_treatment_status",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
