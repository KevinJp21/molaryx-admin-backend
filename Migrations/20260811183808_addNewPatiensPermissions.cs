using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class addNewPatiensPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "role_permissions",
                columns: new[] { "IdPermission", "IdUserRole" },
                values: new object[] { (short)4, (short)2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "IdPermission", "IdUserRole" },
                keyValues: new object[] { (short)4, (short)2 });
        }
    }
}
