using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHouse.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackDevice_User_UserId",
                table: "FeedbackDevice");

            migrationBuilder.DropForeignKey(
                name: "FK_NuemericalFeedbackDevice_User_UserId",
                table: "NuemericalFeedbackDevice");

            migrationBuilder.DropForeignKey(
                name: "FK_NuemericalSensor_User_UserId",
                table: "NuemericalSensor");

            migrationBuilder.DropForeignKey(
                name: "FK_Sensor_User_UserId",
                table: "Sensor");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackDevice_User_UserId",
                table: "FeedbackDevice",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_NuemericalFeedbackDevice_User_UserId",
                table: "NuemericalFeedbackDevice",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_NuemericalSensor_User_UserId",
                table: "NuemericalSensor",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Sensor_User_UserId",
                table: "Sensor",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackDevice_User_UserId",
                table: "FeedbackDevice");

            migrationBuilder.DropForeignKey(
                name: "FK_NuemericalFeedbackDevice_User_UserId",
                table: "NuemericalFeedbackDevice");

            migrationBuilder.DropForeignKey(
                name: "FK_NuemericalSensor_User_UserId",
                table: "NuemericalSensor");

            migrationBuilder.DropForeignKey(
                name: "FK_Sensor_User_UserId",
                table: "Sensor");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackDevice_User_UserId",
                table: "FeedbackDevice",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NuemericalFeedbackDevice_User_UserId",
                table: "NuemericalFeedbackDevice",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NuemericalSensor_User_UserId",
                table: "NuemericalSensor",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sensor_User_UserId",
                table: "Sensor",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
