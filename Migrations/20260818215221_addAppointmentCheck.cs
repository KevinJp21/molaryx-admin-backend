using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class addAppointmentCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "appointments",
                type: "numeric(12,2)",
                precision: 12,
                scale: 2,
                nullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "ck_appointments_treatment_or_price",
                table: "appointments",
                sql: "(\r\n    id_patient_treatment IS NULL\r\n    OR price IS NULL\r\n)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "ck_appointments_treatment_or_price",
                table: "appointments");

            migrationBuilder.DropColumn(
                name: "price",
                table: "appointments");
        }
    }
}
