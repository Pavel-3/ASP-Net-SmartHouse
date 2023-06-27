using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHouse.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserDefinedName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserDefinedName",
                table: "Sensor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserDefinedName",
                table: "NuemericalSensor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserDefinedName",
                table: "NuemericalFeedbackDevice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserDefinedName",
                table: "FeedbackDevice",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserDefinedName",
                table: "Sensor");

            migrationBuilder.DropColumn(
                name: "UserDefinedName",
                table: "NuemericalSensor");

            migrationBuilder.DropColumn(
                name: "UserDefinedName",
                table: "NuemericalFeedbackDevice");

            migrationBuilder.DropColumn(
                name: "UserDefinedName",
                table: "FeedbackDevice");
        }
    }
}
