using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class updateRolPermissionsSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)1,
                column: "code",
                value: "GET_PF_TENANTS");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)2,
                column: "code",
                value: "ACTIVATE_PF_TENANT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)3,
                column: "code",
                value: "CREATE_PF_BUSINESS_TENANT");

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id_permission", "code", "created_at", "id_module", "updated_at" },
                values: new object[] { (short)4, "GET_USER_PATIENTS", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), (short)3, null });

            migrationBuilder.InsertData(
                table: "role_permissions",
                columns: new[] { "IdPermission", "IdUserRole" },
                values: new object[] { (short)3, (short)1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)4);

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)3, (short)1 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)1,
                column: "code",
                value: "GET_TENANTS");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)2,
                column: "code",
                value: "ACTIVATE_TENANT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)3,
                column: "code",
                value: "CREATE_BUSINESS_TENANT");
        }
    }
}
