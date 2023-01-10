using Microsoft.EntityFrameworkCore.Migrations;

namespace UserRegistrationDotNetCore.Migrations
{
    public partial class Fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FacilityOfRoomId",
                table: "Rooms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_FacilityOfRoomId",
                table: "Rooms",
                column: "FacilityOfRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomFacilities_FacilityOfRoomId",
                table: "Rooms",
                column: "FacilityOfRoomId",
                principalTable: "RoomFacilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomFacilities_FacilityOfRoomId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_FacilityOfRoomId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "FacilityOfRoomId",
                table: "Rooms");
        }
    }
}
