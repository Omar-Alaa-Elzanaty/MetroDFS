using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetroDFS.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class AddLineDistance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Distance",
                table: "ChildrensStations",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distance",
                table: "ChildrensStations");
        }
    }
}
