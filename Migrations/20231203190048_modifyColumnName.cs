using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eventsback.Migrations
{
    /// <inheritdoc />
    public partial class modifyColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IDW",
                table: "Participant",
                newName: "IDWControl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IDWControl",
                table: "Participant",
                newName: "IDW");
        }
    }
}
