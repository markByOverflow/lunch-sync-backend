using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LunchSync.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "collections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    landmark_lat = table.Column<double>(type: "double precision", nullable: true),
                    landmark_lng = table.Column<double>(type: "double precision", nullable: true),
                    coverage_radius_meters = table.Column<int>(type: "integer", nullable: false, defaultValue: 250),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "Active"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_collections", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dishes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    profile = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dishes", x => x.id);
                    table.CheckConstraint("chk_profile_valid", "jsonb_typeof(profile) = 'object'");
                });

            migrationBuilder.CreateTable(
                name: "restaurants",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    google_maps_url = table.Column<string>(type: "text", nullable: true),
                    price_tier = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    rating = table.Column<double>(type: "double precision", nullable: true),
                    thumbnail_url = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "Active"),
                    source = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "Admin"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_restaurants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    cognito_sub = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    full_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    role = table.Column<string>(type: "text", nullable: false, defaultValue: "User"),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "restaurant_collections",
                columns: table => new
                {
                    restaurant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    collection_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_restaurant_collections", x => new { x.restaurant_id, x.collection_id });
                    table.ForeignKey(
                        name: "fk_restaurant_collections_collections_collection_id",
                        column: x => x.collection_id,
                        principalTable: "collections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_restaurant_collections_restaurants_restaurant_id",
                        column: x => x.restaurant_id,
                        principalTable: "restaurants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "restaurant_dishes",
                columns: table => new
                {
                    restaurant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    dish_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_restaurant_dishes", x => new { x.restaurant_id, x.dish_id });
                    table.ForeignKey(
                        name: "fk_restaurant_dishes_dishes_dish_id",
                        column: x => x.dish_id,
                        principalTable: "dishes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_restaurant_dishes_restaurants_restaurant_id",
                        column: x => x.restaurant_id,
                        principalTable: "restaurants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    pin = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    host_id = table.Column<Guid>(type: "uuid", nullable: false),
                    collection_id = table.Column<Guid>(type: "uuid", nullable: false),
                    price_tier = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "Waiting"),
                    group_vector = table.Column<List<float>>(type: "jsonb", nullable: true),
                    top_dish_ids = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    top_restaurant_ids = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    boom_eliminated_ids = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    final_restaurant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    voting_started_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sessions", x => x.id);
                    table.ForeignKey(
                        name: "fk_sessions_collections_collection_id",
                        column: x => x.collection_id,
                        principalTable: "collections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_sessions_restaurants_final_restaurant_id",
                        column: x => x.final_restaurant_id,
                        principalTable: "restaurants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_sessions_users_host_id",
                        column: x => x.host_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "submissions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    restaurant_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    google_maps_url = table.Column<string>(type: "text", nullable: true),
                    price_tier = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "Pending"),
                    reviewed_by = table.Column<Guid>(type: "uuid", nullable: true),
                    reviewed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_submissions", x => x.id);
                    table.ForeignKey(
                        name: "fk_submissions_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "participants",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    session_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    nickname = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    pref_vector = table.Column<List<float>>(type: "jsonb", nullable: true),
                    joined_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    voted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_participants", x => x.id);
                    table.ForeignKey(
                        name: "fk_participants_sessions_session_id",
                        column: x => x.session_id,
                        principalTable: "sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_participants_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "submission_dishes",
                columns: table => new
                {
                    submission_id = table.Column<Guid>(type: "uuid", nullable: false),
                    dish_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_submission_dishes", x => new { x.submission_id, x.dish_id });
                    table.ForeignKey(
                        name: "fk_submission_dishes_dishes_dish_id",
                        column: x => x.dish_id,
                        principalTable: "dishes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_submission_dishes_submissions_submission_id",
                        column: x => x.submission_id,
                        principalTable: "submissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "submission_photos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    submission_id = table.Column<Guid>(type: "uuid", nullable: false),
                    photo_url = table.Column<string>(type: "text", nullable: false),
                    display_order = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_submission_photos", x => x.id);
                    table.ForeignKey(
                        name: "fk_submission_photos_submissions_submission_id",
                        column: x => x.submission_id,
                        principalTable: "submissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_dishes_name",
                table: "dishes",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_participants_session",
                table: "participants",
                column: "session_id");

            migrationBuilder.CreateIndex(
                name: "ix_participants_session_id_nickname",
                table: "participants",
                columns: new[] { "session_id", "nickname" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_participants_user_id",
                table: "participants",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "idx_rc_collection",
                table: "restaurant_collections",
                column: "collection_id");

            migrationBuilder.CreateIndex(
                name: "idx_rd_dish",
                table: "restaurant_dishes",
                column: "dish_id");

            migrationBuilder.CreateIndex(
                name: "idx_rd_restaurant",
                table: "restaurant_dishes",
                column: "restaurant_id");

            migrationBuilder.CreateIndex(
                name: "idx_sessions_host",
                table: "sessions",
                column: "host_id");

            migrationBuilder.CreateIndex(
                name: "idx_sessions_pin",
                table: "sessions",
                column: "pin");

            migrationBuilder.CreateIndex(
                name: "idx_sessions_status",
                table: "sessions",
                column: "status",
                filter: "status NOT IN ('done')");

            migrationBuilder.CreateIndex(
                name: "ix_sessions_collection_id",
                table: "sessions",
                column: "collection_id");

            migrationBuilder.CreateIndex(
                name: "ix_sessions_final_restaurant_id",
                table: "sessions",
                column: "final_restaurant_id");

            migrationBuilder.CreateIndex(
                name: "ix_submission_dishes_dish_id",
                table: "submission_dishes",
                column: "dish_id");

            migrationBuilder.CreateIndex(
                name: "idx_submission_photos_sub",
                table: "submission_photos",
                column: "submission_id");

            migrationBuilder.CreateIndex(
                name: "idx_submissions_status",
                table: "submissions",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "idx_submissions_user",
                table: "submissions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "idx_users_cognito_sub",
                table: "users",
                column: "cognito_sub",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_users_role",
                table: "users",
                column: "role");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "participants");

            migrationBuilder.DropTable(
                name: "restaurant_collections");

            migrationBuilder.DropTable(
                name: "restaurant_dishes");

            migrationBuilder.DropTable(
                name: "submission_dishes");

            migrationBuilder.DropTable(
                name: "submission_photos");

            migrationBuilder.DropTable(
                name: "sessions");

            migrationBuilder.DropTable(
                name: "dishes");

            migrationBuilder.DropTable(
                name: "submissions");

            migrationBuilder.DropTable(
                name: "collections");

            migrationBuilder.DropTable(
                name: "restaurants");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
