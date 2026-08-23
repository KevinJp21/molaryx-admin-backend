using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class addCreateMemberPerAdDeleteAtInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                table: "users",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)16,
                columns: new[] { "code", "id_module" },
                values: new object[] { "CREATE_MEMBER", (short)2 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)17,
                column: "code",
                value: "GET_TREATMENTS");

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
                columns: new[] { "code", "id_module" },
                values: new object[] { "DELETE_TREATMENT", (short)6 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)21,
                column: "code",
                value: "CREATE_PATIENT_TREATMENT");

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
                columns: new[] { "code", "id_module" },
                values: new object[] { "UPDATE_PATIENT_TREATMENT", (short)7 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)24,
                column: "code",
                value: "CREATE_PAYMENT");

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
                columns: new[] { "code", "id_module" },
                values: new object[] { "GET_PAYMENTS_SUMMARY_BY_CONCEPT", (short)8 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)27,
                column: "code",
                value: "CREATE_CLINICAL_RECORD");

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id_permission", "code", "created_at", "id_module", "updated_at" },
                values: new object[] { (short)28, "GET_CLINICAL_RECORDS", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), (short)9, null });

            migrationBuilder.InsertData(
                table: "role_permissions",
                columns: new[] { "IdPermission", "IdUserRole" },
                values: new object[] { (short)28, (short)2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)28, (short)2 });

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)28);

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "users");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)16,
                columns: new[] { "code", "id_module" },
                values: new object[] { "GET_TREATMENTS", (short)6 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)17,
                column: "code",
                value: "CREATE_TREATMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)18,
                column: "code",
                value: "UPDATE_TREATMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)19,
                column: "code",
                value: "DELETE_TREATMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)20,
                columns: new[] { "code", "id_module" },
                values: new object[] { "CREATE_PATIENT_TREATMENT", (short)7 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)21,
                column: "code",
                value: "GET_PATIENT_TREATMENTS");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)22,
                column: "code",
                value: "UPDATE_PATIENT_TREATMENT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)23,
                columns: new[] { "code", "id_module" },
                values: new object[] { "CREATE_PAYMENT", (short)8 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)24,
                column: "code",
                value: "GET_PAYMENTS");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)25,
                column: "code",
                value: "GET_PAYMENTS_SUMMARY_BY_CONCEPT");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)26,
                columns: new[] { "code", "id_module" },
                values: new object[] { "CREATE_CLINICAL_RECORD", (short)9 });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)27,
                column: "code",
                value: "GET_CLINICAL_RECORDS");
        }
    }
}
