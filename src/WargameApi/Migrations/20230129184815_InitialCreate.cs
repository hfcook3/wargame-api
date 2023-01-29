using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WargameApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KillTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KillTeams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Attacks = table.Column<int>(type: "integer", nullable: false),
                    SkillThreshold = table.Column<int>(type: "integer", nullable: false),
                    NormalDamage = table.Column<int>(type: "integer", nullable: false),
                    CritDamage = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operatives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MovementDistance = table.Column<int>(type: "integer", nullable: false),
                    MovementCount = table.Column<int>(type: "integer", nullable: false),
                    ActionPointLimit = table.Column<int>(type: "integer", nullable: false),
                    GroupActivation = table.Column<int>(type: "integer", nullable: false),
                    Defense = table.Column<int>(type: "integer", nullable: false),
                    SaveThreshold = table.Column<int>(type: "integer", nullable: false),
                    Wounds = table.Column<int>(type: "integer", nullable: false),
                    KillTeamId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operatives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operatives_KillTeams_KillTeamId",
                        column: x => x.KillTeamId,
                        principalTable: "KillTeams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CriticalHitRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    WeaponId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriticalHitRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriticalHitRule_Weapons_WeaponId",
                        column: x => x.WeaponId,
                        principalTable: "Weapons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActionPointCost = table.Column<int>(type: "integer", nullable: false),
                    Rule = table.Column<string>(type: "text", nullable: false),
                    OperativeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actions_Operatives_OperativeId",
                        column: x => x.OperativeId,
                        principalTable: "Operatives",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Keyword",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    OperativeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keyword", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Keyword_Operatives_OperativeId",
                        column: x => x.OperativeId,
                        principalTable: "Operatives",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OperativeWeapon",
                columns: table => new
                {
                    OperativesId = table.Column<int>(type: "integer", nullable: false),
                    WeaponsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperativeWeapon", x => new { x.OperativesId, x.WeaponsId });
                    table.ForeignKey(
                        name: "FK_OperativeWeapon_Operatives_OperativesId",
                        column: x => x.OperativesId,
                        principalTable: "Operatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperativeWeapon_Weapons_WeaponsId",
                        column: x => x.WeaponsId,
                        principalTable: "Weapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecialRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    OperativeId = table.Column<int>(type: "integer", nullable: true),
                    WeaponId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialRule_Operatives_OperativeId",
                        column: x => x.OperativeId,
                        principalTable: "Operatives",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialRule_Weapons_WeaponId",
                        column: x => x.WeaponId,
                        principalTable: "Weapons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actions_OperativeId",
                table: "Actions",
                column: "OperativeId");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalHitRule_WeaponId",
                table: "CriticalHitRule",
                column: "WeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyword_OperativeId",
                table: "Keyword",
                column: "OperativeId");

            migrationBuilder.CreateIndex(
                name: "IX_Operatives_KillTeamId",
                table: "Operatives",
                column: "KillTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_OperativeWeapon_WeaponsId",
                table: "OperativeWeapon",
                column: "WeaponsId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialRule_OperativeId",
                table: "SpecialRule",
                column: "OperativeId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialRule_WeaponId",
                table: "SpecialRule",
                column: "WeaponId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "CriticalHitRule");

            migrationBuilder.DropTable(
                name: "Keyword");

            migrationBuilder.DropTable(
                name: "OperativeWeapon");

            migrationBuilder.DropTable(
                name: "SpecialRule");

            migrationBuilder.DropTable(
                name: "Operatives");

            migrationBuilder.DropTable(
                name: "Weapons");

            migrationBuilder.DropTable(
                name: "KillTeams");
        }
    }
}
