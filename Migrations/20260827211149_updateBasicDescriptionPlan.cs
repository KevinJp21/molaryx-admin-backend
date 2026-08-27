using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class updateBasicDescriptionPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "plans",
                keyColumn: "id_plan",
                keyValue: (short)1,
                column: "description",
                value: "Plan esencial para consultorios que buscan organizar su gestión.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "plans",
                keyColumn: "id_plan",
                keyValue: (short)1,
                column: "description",
                value: "Plan básico para consultorios pequeños.");
        }
    }
}
