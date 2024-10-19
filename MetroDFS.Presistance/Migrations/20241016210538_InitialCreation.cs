using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetroDFS.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChildrensStations",
                columns: table => new
                {
                    StationId = table.Column<int>(type: "int", nullable: false),
                    ParentStationId = table.Column<int>(type: "int", nullable: false),
                    Line = table.Column<byte>(type: "tinyint", nullable: false),
                    LineDirection = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildrensStations", x => new { x.StationId, x.ParentStationId });
                    table.ForeignKey(
                        name: "FK_ChildrensStations_Stations_ParentStationId",
                        column: x => x.ParentStationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChildrensStations_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChildrensStations_ParentStationId",
                table: "ChildrensStations",
                column: "ParentStationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildrensStations");

            migrationBuilder.DropTable(
                name: "Stations");
        }
    }
}
