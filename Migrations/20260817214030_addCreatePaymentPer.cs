using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class addCreatePaymentPer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "modules",
                columns: new[] { "id_module", "code", "created_at", "updated_at" },
                values: new object[] { (short)8, "PAYMENTS", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), null });

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id_permission", "code", "created_at", "id_module", "updated_at" },
                values: new object[] { (short)23, "CREATE_PAYMENT", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), (short)8, null });

            migrationBuilder.InsertData(
                table: "role_permissions",
                columns: new[] { "IdPermission", "IdUserRole" },
                values: new object[] { (short)23, (short)2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)23, (short)2 });

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)23);

            migrationBuilder.DeleteData(
                table: "modules",
                keyColumn: "id_module",
                keyValue: (short)8);
        }
    }
}
