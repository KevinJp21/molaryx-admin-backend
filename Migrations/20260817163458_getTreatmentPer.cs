using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class getTreatmentPer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // HasData reorders existing permission ids. Unique code must be
            // vacated before assigning the target codes to those same ids.
            migrationBuilder.Sql("""
                UPDATE permissions
                SET code = 'TMP_PERM_' || id_permission::text
                WHERE id_permission IN (14, 15, 16, 17);
                """);

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)14,
                columns: new[] { "code", "id_module" },
                values: new object[] { "UPDATE_APPOINTMENT", (short)5 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)15,
                columns: new[] { "code", "id_module" },
                values: new object[] { "GET_PROFESSIONALS", (short)2 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)16,
                column: "code",
                value: "GET_TREATMENTS");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)17,
                column: "code",
                value: "CREATE_TREATMENT");

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id_permission", "code", "created_at", "id_module", "updated_at" },
                values: new object[] { (short)18, "UPDATE_TREATMENT", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), (short)6, null });

            migrationBuilder.InsertData(
                table: "role_permissions",
                columns: new[] { "IdPermission", "IdUserRole" },
                values: new object[] { (short)18, (short)2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)18, (short)2 });

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)18);

            migrationBuilder.Sql("""
                UPDATE permissions
                SET code = 'TMP_PERM_' || id_permission::text
                WHERE id_permission IN (14, 15, 16, 17);
                """);

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)14,
                columns: new[] { "code", "id_module" },
                values: new object[] { "GET_PROFESSIONALS", (short)2 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)15,
                columns: new[] { "code", "id_module" },
                values: new object[] { "UPDATE_APPOINTMENT", (short)5 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)16,
                column: "code",
                value: "CREATE_TREATMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)17,
                column: "code",
                value: "UPDATE_TREATMENT");
        }
    }
}
