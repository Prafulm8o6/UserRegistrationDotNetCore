using Microsoft.EntityFrameworkCore.Migrations;

namespace UserRegistrationDotNetCore.Migrations
{
    public partial class Sixth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Description",
                table: "RoomFacilities");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "RoomFacilities");

            migrationBuilder.AddColumn<int>(
                name: "FacilityId",
                table: "RoomFacilities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "RoomFacilities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomFacilities_FacilityId",
                table: "RoomFacilities",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomFacilities_RoomId",
                table: "RoomFacilities",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFacilities_Facilities_FacilityId",
                table: "RoomFacilities",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFacilities_Rooms_RoomId",
                table: "RoomFacilities",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomFacilities_Facilities_FacilityId",
                table: "RoomFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomFacilities_Rooms_RoomId",
                table: "RoomFacilities");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropIndex(
                name: "IX_RoomFacilities_FacilityId",
                table: "RoomFacilities");

            migrationBuilder.DropIndex(
                name: "IX_RoomFacilities_RoomId",
                table: "RoomFacilities");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "RoomFacilities");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "RoomFacilities");

            migrationBuilder.AddColumn<int>(
                name: "FacilityOfRoomId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RoomFacilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "RoomFacilities",
                type: "nvarchar(max)",
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
    }
}
