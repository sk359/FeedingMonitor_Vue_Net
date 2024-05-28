using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "feeding",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    catname = table.Column<string>(type: "text", nullable: false),
                    brandname = table.Column<string>(type: "text", nullable: false),
                    productname = table.Column<string>(type: "text", nullable: false),
                    taste = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    eatenpercentage = table.Column<int>(type: "integer", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    feedingtime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feeding", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stamm_brand",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stamm_brand", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stamm_product",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    food_type = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    stamm_brand_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stamm_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_stamm_product_stamm_brand_stamm_brand_id",
                        column: x => x.stamm_brand_id,
                        principalTable: "stamm_brand",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_stamm_product_stamm_brand_id",
                table: "stamm_product",
                column: "stamm_brand_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "feeding");

            migrationBuilder.DropTable(
                name: "stamm_product");

            migrationBuilder.DropTable(
                name: "stamm_brand");
        }
    }
}
