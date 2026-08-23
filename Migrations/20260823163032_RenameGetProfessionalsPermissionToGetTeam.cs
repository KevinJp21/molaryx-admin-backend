using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class RenameGetProfessionalsPermissionToGetTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                UPDATE permissions
                SET code = 'TMP_PERM_' || id_permission::text
                WHERE id_permission = 15;
                """);

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)15,
                column: "code",
                value: "GET_TEAM");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                UPDATE permissions
                SET code = 'TMP_PERM_' || id_permission::text
                WHERE id_permission = 15;
                """);

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id_permission",
                keyValue: (short)15,
                column: "code",
                value: "GET_PROFESSIONALS");
        }
    }
}
