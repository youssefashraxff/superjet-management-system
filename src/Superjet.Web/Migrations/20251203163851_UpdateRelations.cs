using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Superjet.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_BusRoutes_BusRouteId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_UserId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "BusRouteId",
                table: "Tickets",
                newName: "busrouteId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_BusRouteId",
                table: "Tickets",
                newName: "IX_Tickets_busrouteId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Tickets",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateTable(
                name: "BusesRoutes",
                columns: table => new
                {
                    BusRoutesId = table.Column<int>(type: "INTEGER", nullable: false),
                    BusesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusesRoutes", x => new { x.BusRoutesId, x.BusesId });
                    table.ForeignKey(
                        name: "FK_BusesRoutes_BusRoutes_BusRoutesId",
                        column: x => x.BusRoutesId,
                        principalTable: "BusRoutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusesRoutes_Buses_BusesId",
                        column: x => x.BusesId,
                        principalTable: "Buses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusesRoutes_BusesId",
                table: "BusesRoutes",
                column: "BusesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_BusRoutes_Id",
                table: "Tickets",
                column: "Id",
                principalTable: "BusRoutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_BusRoutes_busrouteId",
                table: "Tickets",
                column: "busrouteId",
                principalTable: "BusRoutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Discounts_Id",
                table: "Tickets",
                column: "Id",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_UserId",
                table: "Tickets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_BusRoutes_Id",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_BusRoutes_busrouteId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Discounts_Id",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_UserId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "BusesRoutes");

            migrationBuilder.RenameColumn(
                name: "busrouteId",
                table: "Tickets",
                newName: "BusRouteId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_busrouteId",
                table: "Tickets",
                newName: "IX_Tickets_BusRouteId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Tickets",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_BusRoutes_BusRouteId",
                table: "Tickets",
                column: "BusRouteId",
                principalTable: "BusRoutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_UserId",
                table: "Tickets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
