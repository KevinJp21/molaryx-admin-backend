using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatePfTenantPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)4, (short)2 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)4, (short)4 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)8, (short)4 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)12, (short)4 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)18, (short)4 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)23, (short)4 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)29, (short)4 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)4, (short)5 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)8, (short)5 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)12, (short)5 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)18, (short)5 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)24, (short)5 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)4,
                columns: new[] { "code", "id_module" },
                values: new object[] { "UPDATE_PF_TENANT", (short)1 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)5,
                column: "code",
                value: "GET_PATIENTS");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)6,
                column: "code",
                value: "CREATE_PATIENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)7,
                column: "code",
                value: "UPDATE_PATIENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)8,
                columns: new[] { "code", "id_module" },
                values: new object[] { "DELETE_PATIENT", (short)3 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)9,
                column: "code",
                value: "GET_PROCEDURES");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)10,
                column: "code",
                value: "CREATE_PROCEDURE");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)11,
                column: "code",
                value: "UPDATE_PROCEDURE");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)12,
                columns: new[] { "code", "id_module" },
                values: new object[] { "DELETE_PROCEDURE", (short)4 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)13,
                column: "code",
                value: "GET_APPOINTMENTS");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)14,
                column: "code",
                value: "CREATE_APPOINTMENT");

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
                value: "GET_TEAM");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)17,
                column: "code",
                value: "CREATE_MEMBER");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)18,
                column: "code",
                value: "UPDATE_MEMBER");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)19,
                columns: new[] { "code", "id_module" },
                values: new object[] { "GET_PROFILE", (short)2 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)20,
                column: "code",
                value: "GET_TREATMENTS");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)21,
                column: "code",
                value: "CREATE_TREATMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)22,
                column: "code",
                value: "UPDATE_TREATMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)23,
                columns: new[] { "code", "id_module" },
                values: new object[] { "DELETE_TREATMENT", (short)6 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)24,
                column: "code",
                value: "CREATE_PATIENT_TREATMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)25,
                column: "code",
                value: "GET_PATIENT_TREATMENTS");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)26,
                columns: new[] { "code", "id_module" },
                values: new object[] { "UPDATE_PATIENT_TREATMENT", (short)7 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)27,
                column: "code",
                value: "CREATE_PAYMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)28,
                column: "code",
                value: "GET_PAYMENTS");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)29,
                columns: new[] { "code", "id_module" },
                values: new object[] { "GET_PAYMENTS_SUMMARY_BY_CONCEPT", (short)8 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)30,
                column: "code",
                value: "CREATE_CLINICAL_RECORD");

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id_permission", "code", "created_at", "id_module", "updated_at" },
                values: new object[] { (short)31, "GET_CLINICAL_RECORDS", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), (short)9, null });

            migrationBuilder.InsertData(
                table: "role_permissions",
                columns: new[] { "IdPermission", "IdUserRole" },
                values: new object[,]
                {
                    { (short)4, (short)1 },
                    { (short)7, (short)4 },
                    { (short)9, (short)4 },
                    { (short)16, (short)4 },
                    { (short)20, (short)4 },
                    { (short)26, (short)4 },
                    { (short)7, (short)5 },
                    { (short)9, (short)5 },
                    { (short)16, (short)5 },
                    { (short)20, (short)5 },
                    { (short)25, (short)5 },
                    { (short)31, (short)2 },
                    { (short)31, (short)4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)4, (short)1 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)31, (short)2 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)7, (short)4 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)9, (short)4 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)16, (short)4 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)20, (short)4 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)26, (short)4 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)31, (short)4 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)7, (short)5 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)9, (short)5 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)16, (short)5 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)20, (short)5 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)25, (short)5 });

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)31);

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)4,
                columns: new[] { "code", "id_module" },
                values: new object[] { "GET_PATIENTS", (short)3 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)5,
                column: "code",
                value: "CREATE_PATIENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)6,
                column: "code",
                value: "UPDATE_PATIENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)7,
                column: "code",
                value: "DELETE_PATIENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)8,
                columns: new[] { "code", "id_module" },
                values: new object[] { "GET_PROCEDURES", (short)4 });

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

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)12,
                columns: new[] { "code", "id_module" },
                values: new object[] { "GET_APPOINTMENTS", (short)5 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)13,
                column: "code",
                value: "CREATE_APPOINTMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)14,
                column: "code",
                value: "UPDATE_APPOINTMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)15,
                columns: new[] { "code", "id_module" },
                values: new object[] { "GET_TEAM", (short)2 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)16,
                column: "code",
                value: "CREATE_MEMBER");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)17,
                column: "code",
                value: "UPDATE_MEMBER");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)18,
                column: "code",
                value: "GET_PROFILE");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)19,
                columns: new[] { "code", "id_module" },
                values: new object[] { "GET_TREATMENTS", (short)6 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)20,
                column: "code",
                value: "CREATE_TREATMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)21,
                column: "code",
                value: "UPDATE_TREATMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)22,
                column: "code",
                value: "DELETE_TREATMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)23,
                columns: new[] { "code", "id_module" },
                values: new object[] { "CREATE_PATIENT_TREATMENT", (short)7 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)24,
                column: "code",
                value: "GET_PATIENT_TREATMENTS");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)25,
                column: "code",
                value: "UPDATE_PATIENT_TREATMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)26,
                columns: new[] { "code", "id_module" },
                values: new object[] { "CREATE_PAYMENT", (short)8 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)27,
                column: "code",
                value: "GET_PAYMENTS");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)28,
                column: "code",
                value: "GET_PAYMENTS_SUMMARY_BY_CONCEPT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)29,
                columns: new[] { "code", "id_module" },
                values: new object[] { "CREATE_CLINICAL_RECORD", (short)9 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)30,
                column: "code",
                value: "GET_CLINICAL_RECORDS");

            migrationBuilder.InsertData(
                table: "role_permissions",
                columns: new[] { "IdPermission", "IdUserRole" },
                values: new object[,]
                {
                    { (short)4, (short)2 },
                    { (short)4, (short)4 },
                    { (short)8, (short)4 },
                    { (short)12, (short)4 },
                    { (short)18, (short)4 },
                    { (short)23, (short)4 },
                    { (short)29, (short)4 },
                    { (short)4, (short)5 },
                    { (short)8, (short)5 },
                    { (short)12, (short)5 },
                    { (short)18, (short)5 },
                    { (short)24, (short)5 }
                });
        }
    }
}
