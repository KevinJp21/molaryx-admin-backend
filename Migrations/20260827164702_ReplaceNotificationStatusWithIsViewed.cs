using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceNotificationStatusWithIsViewed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_notifications_id_tenant_id_user_id_notification_status",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "id_notification_status",
                table: "notifications");

            migrationBuilder.AddColumn<bool>(
                name: "is_viewed",
                table: "notifications",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_notifications_id_tenant_id_user_is_viewed",
                table: "notifications",
                columns: new[] { "id_tenant", "id_user", "is_viewed" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_notifications_id_tenant_id_user_is_viewed",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "is_viewed",
                table: "notifications");

            migrationBuilder.AddColumn<short>(
                name: "id_notification_status",
                table: "notifications",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateIndex(
                name: "IX_notifications_id_tenant_id_user_id_notification_status",
                table: "notifications",
                columns: new[] { "id_tenant", "id_user", "id_notification_status" });
        }
    }
}
