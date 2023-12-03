using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eventsback.Migrations
{
    /// <inheritdoc />
    public partial class identifyUniqueColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IDWControl",
                table: "Participant",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Control",
                table: "Participant",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participant_Control",
                table: "Participant",
                column: "Control",
                unique: true,
                filter: "[Control] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_IDWControl",
                table: "Participant",
                column: "IDWControl",
                unique: true,
                filter: "[IDWControl] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Participant_Control",
                table: "Participant");

            migrationBuilder.DropIndex(
                name: "IX_Participant_IDWControl",
                table: "Participant");

            migrationBuilder.AlterColumn<string>(
                name: "IDWControl",
                table: "Participant",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Control",
                table: "Participant",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
