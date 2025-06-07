using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace squares_api_adform.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexToPoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Points_X_Y",
                table: "Points",
                columns: new[] { "X", "Y" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Points_X_Y",
                table: "Points");
        }
    }
}
