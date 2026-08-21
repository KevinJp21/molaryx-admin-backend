using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace molaryxadmin.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "identification_types",
                columns: table => new
                {
                    id_identification_type = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identification_types", x => x.id_identification_type);
                });

            migrationBuilder.CreateTable(
                name: "modules",
                columns: table => new
                {
                    id_module = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modules", x => x.id_module);
                });

            migrationBuilder.CreateTable(
                name: "plans",
                columns: table => new
                {
                    id_plan = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<decimal>(type: "numeric", nullable: true),
                    max_professionals = table.Column<short>(type: "smallint", nullable: true),
                    max_assistants = table.Column<short>(type: "smallint", nullable: true),
                    max_patients = table.Column<int>(type: "integer", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plans", x => x.id_plan);
                });

            migrationBuilder.CreateTable(
                name: "tenant_statuses",
                columns: table => new
                {
                    id_tenant_status = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenant_statuses", x => x.id_tenant_status);
                });

            migrationBuilder.CreateTable(
                name: "tenant_subscription_statuses",
                columns: table => new
                {
                    id_tenant_subscription_status = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenant_subscription_statuses", x => x.id_tenant_subscription_status);
                });

            migrationBuilder.CreateTable(
                name: "tenant_types",
                columns: table => new
                {
                    id_tenant_type = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenant_types", x => x.id_tenant_type);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    id_user_role = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => x.id_user_role);
                });

            migrationBuilder.CreateTable(
                name: "user_statuses",
                columns: table => new
                {
                    id_user_status = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_statuses", x => x.id_user_status);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    id_permission = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_module = table.Column<short>(type: "smallint", nullable: false),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.id_permission);
                    table.ForeignKey(
                        name: "FK_permissions_modules_id_module",
                        column: x => x.id_module,
                        principalTable: "modules",
                        principalColumn: "id_module",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "promotions",
                columns: table => new
                {
                    id_promotion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_tenant_type = table.Column<short>(type: "smallint", nullable: true),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    starts_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ends_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    duration_months = table.Column<short>(type: "smallint", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promotions", x => x.id_promotion);
                    table.ForeignKey(
                        name: "FK_promotions_tenant_types_id_tenant_type",
                        column: x => x.id_tenant_type,
                        principalTable: "tenant_types",
                        principalColumn: "id_tenant_type",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tenants",
                columns: table => new
                {
                    id_tenant = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_tenant_type = table.Column<short>(type: "smallint", nullable: false),
                    id_tenant_status = table.Column<short>(type: "smallint", nullable: false),
                    id_identification_type = table.Column<short>(type: "smallint", nullable: true),
                    identification_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    consultory_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenants", x => x.id_tenant);
                    table.ForeignKey(
                        name: "FK_tenants_identification_types_id_identification_type",
                        column: x => x.id_identification_type,
                        principalTable: "identification_types",
                        principalColumn: "id_identification_type",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tenants_tenant_statuses_id_tenant_status",
                        column: x => x.id_tenant_status,
                        principalTable: "tenant_statuses",
                        principalColumn: "id_tenant_status",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tenants_tenant_types_id_tenant_type",
                        column: x => x.id_tenant_type,
                        principalTable: "tenant_types",
                        principalColumn: "id_tenant_type",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                columns: table => new
                {
                    IdUserRole = table.Column<short>(type: "smallint", nullable: false),
                    IdPermission = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permissions", x => new { x.IdUserRole, x.IdPermission });
                    table.ForeignKey(
                        name: "FK_role_permissions_permissions_IdPermission",
                        column: x => x.IdPermission,
                        principalTable: "permissions",
                        principalColumn: "id_permission",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_role_permissions_user_roles_IdUserRole",
                        column: x => x.IdUserRole,
                        principalTable: "user_roles",
                        principalColumn: "id_user_role",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "promotion_plans",
                columns: table => new
                {
                    id_promotion = table.Column<long>(type: "bigint", nullable: false),
                    id_plan = table.Column<short>(type: "smallint", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promotion_plans", x => new { x.id_promotion, x.id_plan });
                    table.ForeignKey(
                        name: "FK_promotion_plans_plans_id_plan",
                        column: x => x.id_plan,
                        principalTable: "plans",
                        principalColumn: "id_plan",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_promotion_plans_promotions_id_promotion",
                        column: x => x.id_promotion,
                        principalTable: "promotions",
                        principalColumn: "id_promotion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tenant_subscriptions",
                columns: table => new
                {
                    id_tenant_subscription = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_tenant_subscription_status = table.Column<short>(type: "smallint", nullable: false),
                    id_tenant = table.Column<long>(type: "bigint", nullable: false),
                    id_plan = table.Column<short>(type: "smallint", nullable: false),
                    id_promotion = table.Column<long>(type: "bigint", nullable: true),
                    price = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    max_professionals = table.Column<short>(type: "smallint", nullable: true),
                    max_assistants = table.Column<short>(type: "smallint", nullable: true),
                    max_patients = table.Column<int>(type: "integer", nullable: true),
                    starts_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ends_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    promotion_ends_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenant_subscriptions", x => x.id_tenant_subscription);
                    table.ForeignKey(
                        name: "FK_tenant_subscriptions_plans_id_plan",
                        column: x => x.id_plan,
                        principalTable: "plans",
                        principalColumn: "id_plan",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tenant_subscriptions_promotions_id_promotion",
                        column: x => x.id_promotion,
                        principalTable: "promotions",
                        principalColumn: "id_promotion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tenant_subscriptions_tenant_subscription_statuses_id_tenant~",
                        column: x => x.id_tenant_subscription_status,
                        principalTable: "tenant_subscription_statuses",
                        principalColumn: "id_tenant_subscription_status",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tenant_subscriptions_tenants_id_tenant",
                        column: x => x.id_tenant,
                        principalTable: "tenants",
                        principalColumn: "id_tenant",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id_user = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_user_status = table.Column<short>(type: "smallint", nullable: false),
                    id_user_rol = table.Column<short>(type: "smallint", nullable: false),
                    id_tenant = table.Column<long>(type: "bigint", nullable: true),
                    username = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    second_name = table.Column<string>(type: "text", nullable: true),
                    first_surname = table.Column<string>(type: "text", nullable: false),
                    second_surname = table.Column<string>(type: "text", nullable: true),
                    id_identification_type = table.Column<short>(type: "smallint", nullable: false),
                    identification_number = table.Column<string>(type: "text", nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<byte[]>(type: "bytea", nullable: false),
                    salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id_user);
                    table.ForeignKey(
                        name: "FK_users_identification_types_id_identification_type",
                        column: x => x.id_identification_type,
                        principalTable: "identification_types",
                        principalColumn: "id_identification_type",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_users_tenants_id_tenant",
                        column: x => x.id_tenant,
                        principalTable: "tenants",
                        principalColumn: "id_tenant",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_users_user_roles_id_user_rol",
                        column: x => x.id_user_rol,
                        principalTable: "user_roles",
                        principalColumn: "id_user_role",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_users_user_statuses_id_user_status",
                        column: x => x.id_user_status,
                        principalTable: "user_statuses",
                        principalColumn: "id_user_status",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "password_reset_tokens",
                columns: table => new
                {
                    id_password_reset_token = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_user = table.Column<long>(type: "bigint", nullable: false),
                    token = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    expires_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    used_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_password_reset_tokens", x => x.id_password_reset_token);
                    table.ForeignKey(
                        name: "FK_password_reset_tokens_users_id_user",
                        column: x => x.id_user,
                        principalTable: "users",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_sessions",
                columns: table => new
                {
                    id_user_session = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_user = table.Column<long>(type: "bigint", nullable: false),
                    refresh_token_hash = table.Column<string>(type: "text", nullable: true),
                    expires_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    revoked_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    device = table.Column<string>(type: "text", nullable: true),
                    ip_connection = table.Column<string>(type: "text", nullable: true),
                    last_login = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_sessions", x => x.id_user_session);
                    table.ForeignKey(
                        name: "FK_user_sessions_users_id_user",
                        column: x => x.id_user,
                        principalTable: "users",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "identification_types",
                columns: new[] { "id_identification_type", "code", "created_at", "name", "updated_at" },
                values: new object[,]
                {
                    { (short)1, "CC", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Cédula de ciudadanía", null },
                    { (short)2, "CE", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Cédula de extranjería", null },
                    { (short)3, "TI", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Tarjeta de identidad", null },
                    { (short)4, "NIT", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Número de Identificación Tributaria", null }
                });

            migrationBuilder.InsertData(
                table: "modules",
                columns: new[] { "id_module", "code", "created_at", "updated_at" },
                values: new object[,]
                {
                    { (short)1, "TENANTS", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { (short)2, "USERS", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { (short)3, "PATIENTS", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), null }
                });

            migrationBuilder.InsertData(
                table: "plans",
                columns: new[] { "id_plan", "code", "created_at", "description", "is_active", "max_assistants", "max_patients", "max_professionals", "name", "price", "updated_at" },
                values: new object[,]
                {
                    { (short)1, "BASIC", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Plan básico para consultorios pequeños.", true, (short)1, 500, (short)1, "Basic", 79900m, null },
                    { (short)2, "PROFESSIONAL", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Plan para consultorios en crecimiento.", true, (short)3, 2000, (short)3, "Professional", 119900m, null },
                    { (short)3, "BUSINESS", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Plan para consultorios con equipos y operaciones de mayor escala.", true, null, null, null, "Business", null, null }
                });

            migrationBuilder.InsertData(
                table: "tenant_statuses",
                columns: new[] { "id_tenant_status", "created_at", "name", "updated_at" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Activo", null },
                    { (short)2, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Inactivo", null },
                    { (short)3, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Pendiente", null },
                    { (short)4, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Bloqueado", null },
                    { (short)5, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Rechazado", null }
                });

            migrationBuilder.InsertData(
                table: "tenant_subscription_statuses",
                columns: new[] { "id_tenant_subscription_status", "code", "created_at", "description", "name", "updated_at" },
                values: new object[,]
                {
                    { (short)1, "PENDING", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "La suscripción está pendiente de activación.", "Pendiente", null },
                    { (short)2, "ACTIVE", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "La suscripción se encuentra activa.", "Activa", null },
                    { (short)3, "SCHEDULED", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "La suscripción ha sido programada para comenzar en una fecha futura.", "Programada", null },
                    { (short)4, "CANCELLED", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "La suscripción ha sido cancelada.", "Cancelada", null },
                    { (short)5, "EXPIRED", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "La suscripción ha expirado.", "Expirada", null },
                    { (short)6, "SUSPENDED", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "La suscripción ha sido suspendida temporalmente.", "Suspendido", null }
                });

            migrationBuilder.InsertData(
                table: "tenant_types",
                columns: new[] { "id_tenant_type", "code", "created_at", "updated_at" },
                values: new object[,]
                {
                    { (short)1, "STANDARD", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { (short)2, "FOUNDER", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), null }
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "id_user_role", "code", "created_at", "description", "name", "updated_at" },
                values: new object[,]
                {
                    { (short)1, "SUPER_ADMIN", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Acceso completo a la plataforma y administración global de todos los consultorios.", "Super Administrador", null },
                    { (short)2, "OWNER", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Acceso completo a la gestión de su consultorio y sus operaciones.", "Propietario", null },
                    { (short)4, "PROFESSIONAL", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Acceso a las funcionalidades clínicas y gestión de la atención de pacientes.", "Profesional", null },
                    { (short)5, "ASSISTANT", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Acceso a las funcionalidades administrativas y operativas asignadas.", "Asistente", null }
                });

            migrationBuilder.InsertData(
                table: "user_statuses",
                columns: new[] { "id_user_status", "created_at", "name", "updated_at" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Activo", null },
                    { (short)2, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Inactivo", null },
                    { (short)3, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Pendiente", null },
                    { (short)4, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Bloqueado", null }
                });

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id_permission", "code", "created_at", "id_module", "updated_at" },
                values: new object[,]
                {
                    { (short)1, "GET_TENANTS", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), (short)1, null },
                    { (short)2, "ACTIVATE_TENANT", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), (short)1, null },
                    { (short)3, "CREATE_BUSINESS_TENANT", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), (short)1, null }
                });

            migrationBuilder.InsertData(
                table: "role_permissions",
                columns: new[] { "IdPermission", "IdUserRole" },
                values: new object[,]
                {
                    { (short)1, (short)1 },
                    { (short)2, (short)1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_modules_code",
                table: "modules",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_password_reset_tokens_id_user",
                table: "password_reset_tokens",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_password_reset_tokens_token",
                table: "password_reset_tokens",
                column: "token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_permissions_code",
                table: "permissions",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_permissions_id_module",
                table: "permissions",
                column: "id_module");

            migrationBuilder.CreateIndex(
                name: "IX_plans_code",
                table: "plans",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_promotion_plans_id_plan",
                table: "promotion_plans",
                column: "id_plan");

            migrationBuilder.CreateIndex(
                name: "IX_promotions_id_tenant_type",
                table: "promotions",
                column: "id_tenant_type");

            migrationBuilder.CreateIndex(
                name: "IX_role_permissions_IdPermission",
                table: "role_permissions",
                column: "IdPermission");

            migrationBuilder.CreateIndex(
                name: "IX_tenant_subscription_statuses_code",
                table: "tenant_subscription_statuses",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tenant_subscriptions_id_plan",
                table: "tenant_subscriptions",
                column: "id_plan");

            migrationBuilder.CreateIndex(
                name: "IX_tenant_subscriptions_id_promotion",
                table: "tenant_subscriptions",
                column: "id_promotion");

            migrationBuilder.CreateIndex(
                name: "IX_tenant_subscriptions_id_tenant",
                table: "tenant_subscriptions",
                column: "id_tenant",
                unique: true,
                filter: "id_tenant_subscription_status = 2");

            migrationBuilder.CreateIndex(
                name: "IX_tenant_subscriptions_id_tenant_subscription_status",
                table: "tenant_subscriptions",
                column: "id_tenant_subscription_status");

            migrationBuilder.CreateIndex(
                name: "IX_tenant_types_code",
                table: "tenant_types",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tenants_email",
                table: "tenants",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tenants_id_identification_type",
                table: "tenants",
                column: "id_identification_type");

            migrationBuilder.CreateIndex(
                name: "IX_tenants_id_tenant_status",
                table: "tenants",
                column: "id_tenant_status");

            migrationBuilder.CreateIndex(
                name: "IX_tenants_id_tenant_type",
                table: "tenants",
                column: "id_tenant_type");

            migrationBuilder.CreateIndex(
                name: "IX_tenants_identification_number",
                table: "tenants",
                column: "identification_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tenants_phone_number",
                table: "tenants",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_name",
                table: "user_roles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_sessions_id_user",
                table: "user_sessions",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_user_sessions_refresh_token_hash",
                table: "user_sessions",
                column: "refresh_token_hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_id_identification_type",
                table: "users",
                column: "id_identification_type");

            migrationBuilder.CreateIndex(
                name: "IX_users_id_tenant",
                table: "users",
                column: "id_tenant");

            migrationBuilder.CreateIndex(
                name: "IX_users_id_user_rol",
                table: "users",
                column: "id_user_rol");

            migrationBuilder.CreateIndex(
                name: "IX_users_id_user_status",
                table: "users",
                column: "id_user_status");

            migrationBuilder.CreateIndex(
                name: "IX_users_identification_number",
                table: "users",
                column: "identification_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_phone_number",
                table: "users",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_username",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "password_reset_tokens");

            migrationBuilder.DropTable(
                name: "promotion_plans");

            migrationBuilder.DropTable(
                name: "role_permissions");

            migrationBuilder.DropTable(
                name: "tenant_subscriptions");

            migrationBuilder.DropTable(
                name: "user_sessions");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "plans");

            migrationBuilder.DropTable(
                name: "promotions");

            migrationBuilder.DropTable(
                name: "tenant_subscription_statuses");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "modules");

            migrationBuilder.DropTable(
                name: "tenants");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "user_statuses");

            migrationBuilder.DropTable(
                name: "identification_types");

            migrationBuilder.DropTable(
                name: "tenant_statuses");

            migrationBuilder.DropTable(
                name: "tenant_types");
        }
    }
}
