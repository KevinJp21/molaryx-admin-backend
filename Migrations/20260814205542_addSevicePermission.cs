using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class addSevicePermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "modules",
                columns: new[] { "id_module", "code", "created_at", "updated_at" },
                values: new object[] { (short)4, "SERVICES", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), null });

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id_permission", "code", "created_at", "id_module", "updated_at" },
                values: new object[,]
                {
                    { (short)8, "GET_SERVICES", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), (short)4, null },
                    { (short)9, "CREATE_SERVICE", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), (short)4, null },
                    { (short)10, "UPDATE_SERVICE", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), (short)4, null },
                    { (short)11, "DELETE_SERVICE", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), (short)4, null }
                });

            migrationBuilder.InsertData(
                table: "role_permissions",
                columns: new[] { "IdPermission", "IdUserRole" },
                values: new object[,]
                {
                    { (short)8, (short)2 },
                    { (short)9, (short)2 },
                    { (short)10, (short)2 },
                    { (short)11, (short)2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)8, (short)2 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)9, (short)2 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)10, (short)2 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)11, (short)2 });

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)8);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)9);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)10);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)11);

            migrationBuilder.DeleteData(
                table: "modules",
                keyColumn: "id_module",
                keyValue: (short)4);
        }
    }
}
