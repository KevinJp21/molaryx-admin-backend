using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatientIdentificationUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_patients_id_tenant_id_identification_type_identification_nu~",
                table: "patients");

            migrationBuilder.CreateIndex(
                name: "IX_patients_id_tenant_identification_number",
                table: "patients",
                columns: new[] { "id_tenant", "identification_number" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_patients_id_tenant_identification_number",
                table: "patients");

            migrationBuilder.CreateIndex(
                name: "IX_patients_id_tenant_id_identification_type_identification_nu~",
                table: "patients",
                columns: new[] { "id_tenant", "id_identification_type", "identification_number" },
                unique: true);
        }
    }
}
