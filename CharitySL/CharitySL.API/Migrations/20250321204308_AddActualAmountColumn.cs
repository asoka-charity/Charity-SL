using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharitySL.API.Migrations
{
    /// <inheritdoc />
    public partial class AddActualAmountColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ActualAmount",
                table: "ProjectContributions",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualAmount",
                table: "ProjectContributions");
        }
    }
}
