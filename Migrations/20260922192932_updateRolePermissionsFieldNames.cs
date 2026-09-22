using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class updateRolePermissionsFieldNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_permissions_permissions_IdPermission",
                table: "role_permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_role_permissions_user_roles_IdUserRole",
                table: "role_permissions");

            migrationBuilder.RenameColumn(
                name: "IdPermission",
                table: "role_permissions",
                newName: "id_permission");

            migrationBuilder.RenameColumn(
                name: "IdUserRole",
                table: "role_permissions",
                newName: "id_user_role");

            migrationBuilder.RenameIndex(
                name: "IX_role_permissions_IdPermission",
                table: "role_permissions",
                newName: "IX_role_permissions_id_permission");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "patients",
                newName: "deleted_at");

            migrationBuilder.AlterColumn<string>(
                name: "second_surname",
                table: "users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "second_name",
                table: "users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "first_surname",
                table: "users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                table: "users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "second_surname",
                table: "patients",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "second_name",
                table: "patients",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "first_surname",
                table: "patients",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                table: "patients",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddForeignKey(
                name: "FK_role_permissions_permissions_id_permission",
                table: "role_permissions",
                column: "id_permission",
                principalTable: "permissions",
                principalColumn: "id_permission",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_role_permissions_user_roles_id_user_role",
                table: "role_permissions",
                column: "id_user_role",
                principalTable: "user_roles",
                principalColumn: "id_user_role",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_permissions_permissions_id_permission",
                table: "role_permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_role_permissions_user_roles_id_user_role",
                table: "role_permissions");

            migrationBuilder.RenameColumn(
                name: "id_permission",
                table: "role_permissions",
                newName: "IdPermission");

            migrationBuilder.RenameColumn(
                name: "id_user_role",
                table: "role_permissions",
                newName: "IdUserRole");

            migrationBuilder.RenameIndex(
                name: "IX_role_permissions_id_permission",
                table: "role_permissions",
                newName: "IX_role_permissions_IdPermission");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "patients",
                newName: "DeletedAt");

            migrationBuilder.AlterColumn<string>(
                name: "second_surname",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "second_name",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "first_surname",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "second_surname",
                table: "patients",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "second_name",
                table: "patients",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "first_surname",
                table: "patients",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                table: "patients",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_role_permissions_permissions_IdPermission",
                table: "role_permissions",
                column: "IdPermission",
                principalTable: "permissions",
                principalColumn: "id_permission",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_role_permissions_user_roles_IdUserRole",
                table: "role_permissions",
                column: "IdUserRole",
                principalTable: "user_roles",
                principalColumn: "id_user_role",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
