using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class addAssistantEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "assistants",
                columns: table => new
                {
                    id_assistant = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_tenant = table.Column<long>(type: "bigint", nullable: false),
                    id_user = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assistants", x => x.id_assistant);
                    table.ForeignKey(
                        name: "FK_assistants_tenants_id_tenant",
                        column: x => x.id_tenant,
                        principalTable: "tenants",
                        principalColumn: "id_tenant",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_assistants_users_id_user",
                        column: x => x.id_user,
                        principalTable: "users",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_assistants_id_tenant_id_assistant",
                table: "assistants",
                columns: new[] { "id_tenant", "id_assistant" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assistants_id_user",
                table: "assistants",
                column: "id_user",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assistants");
        }
    }
}
