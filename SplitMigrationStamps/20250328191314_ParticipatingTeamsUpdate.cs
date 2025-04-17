using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace champsModel.Migrations
{
    /// <inheritdoc />
    public partial class ParticipatingTeamsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParticipatingTeams",
                table: "Tournament",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipatingTeams",
                table: "Tournament");
        }
    }
}
