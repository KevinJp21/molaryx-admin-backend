using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class SeedAssistantRolePermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "role_permissions",
                columns: new[] { "IdPermission", "IdUserRole" },
                values: new object[,]
                {
                    { (short)4, (short)5 },
                    { (short)5, (short)5 },
                    { (short)6, (short)5 },
                    { (short)8, (short)5 },
                    { (short)12, (short)5 },
                    { (short)13, (short)5 },
                    { (short)14, (short)5 },
                    { (short)15, (short)5 },
                    { (short)17, (short)5 },
                    { (short)22, (short)5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)4, (short)5 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)5, (short)5 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)6, (short)5 });

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
                keyValues: new object[] { (short)13, (short)5 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)14, (short)5 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)15, (short)5 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)17, (short)5 });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)22, (short)5 });
        }
    }
}
