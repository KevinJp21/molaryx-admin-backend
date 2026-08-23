using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdateMemberPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)17, (short)4 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)21, (short)4 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)27, (short)4 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)17, (short)5 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)22, (short)5 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)17,
                columns: new[] { "code", "id_module" },
                values: new object[] { "UPDATE_MEMBER", (short)2 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)18,
                column: "code",
                value: "GET_TREATMENTS");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)19,
                column: "code",
                value: "CREATE_TREATMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)20,
                column: "code",
                value: "UPDATE_TREATMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)21,
                columns: new[] { "code", "id_module" },
                values: new object[] { "DELETE_TREATMENT", (short)6 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)22,
                column: "code",
                value: "CREATE_PATIENT_TREATMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)23,
                column: "code",
                value: "GET_PATIENT_TREATMENTS");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)24,
                columns: new[] { "code", "id_module" },
                values: new object[] { "UPDATE_PATIENT_TREATMENT", (short)7 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)25,
                column: "code",
                value: "CREATE_PAYMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)26,
                column: "code",
                value: "GET_PAYMENTS");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)27,
                columns: new[] { "code", "id_module" },
                values: new object[] { "GET_PAYMENTS_SUMMARY_BY_CONCEPT", (short)8 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)28,
                column: "code",
                value: "CREATE_CLINICAL_RECORD");

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id_permission", "code", "created_at", "id_module", "updated_at" },
                values: new object[] { (short)29, "GET_CLINICAL_RECORDS", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), (short)9, null });

            migrationBuilder.InsertData(
                table: "role_permissions",
                columns: new[] { "IdPermission", "IdUserRole" },
                values: new object[,]
                {
                    { (short)18, (short)4 },
                    { (short)24, (short)4 },
                    { (short)18, (short)5 },
                    { (short)23, (short)5 },
                    { (short)29, (short)2 },
                    { (short)29, (short)4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)29, (short)2 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)18, (short)4 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)24, (short)4 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)29, (short)4 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)18, (short)5 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)23, (short)5 });

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)29);

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)17,
                columns: new[] { "code", "id_module" },
                values: new object[] { "GET_TREATMENTS", (short)6 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)18,
                column: "code",
                value: "CREATE_TREATMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)19,
                column: "code",
                value: "UPDATE_TREATMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)20,
                column: "code",
                value: "DELETE_TREATMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)21,
                columns: new[] { "code", "id_module" },
                values: new object[] { "CREATE_PATIENT_TREATMENT", (short)7 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)22,
                column: "code",
                value: "GET_PATIENT_TREATMENTS");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)23,
                column: "code",
                value: "UPDATE_PATIENT_TREATMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)24,
                columns: new[] { "code", "id_module" },
                values: new object[] { "CREATE_PAYMENT", (short)8 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)25,
                column: "code",
                value: "GET_PAYMENTS");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)26,
                column: "code",
                value: "GET_PAYMENTS_SUMMARY_BY_CONCEPT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)27,
                columns: new[] { "code", "id_module" },
                values: new object[] { "CREATE_CLINICAL_RECORD", (short)9 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)28,
                column: "code",
                value: "GET_CLINICAL_RECORDS");

            migrationBuilder.InsertData(
                table: "role_permissions",
                columns: new[] { "IdPermission", "IdUserRole" },
                values: new object[,]
                {
                    { (short)17, (short)4 },
                    { (short)21, (short)4 },
                    { (short)27, (short)4 },
                    { (short)17, (short)5 },
                    { (short)22, (short)5 }
                });
        }
    }
}
