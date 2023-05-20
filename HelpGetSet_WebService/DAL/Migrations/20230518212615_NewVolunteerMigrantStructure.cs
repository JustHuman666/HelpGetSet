using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class NewVolunteerMigrantStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Migrants_Id",
                table: "UserProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Volunteers_Id",
                table: "UserProfiles");

            migrationBuilder.DeleteData(
                table: "Migrants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Volunteers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "MigrantId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "VolunteerId",
                table: "UserProfiles");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Volunteers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Migrants",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "fc88910b-40e6-4d90-99f0-8562856aa89b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5a25ab74-13b2-4535-916e-8b13a3759e8a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d1917337-89ab-4721-81c8-0b1f656b77d8", "AQAAAAEAACcQAAAAEMtgO78mcRjg4Uguy/VpISKbTxpfN/UKMMI34oDo70nC7uVTw+fDAHTPSyPhSLi0Fg==" });

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_UserId",
                table: "Volunteers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Migrants_UserId",
                table: "Migrants",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Migrants_UserProfiles_UserId",
                table: "Migrants",
                column: "UserId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Volunteers_UserProfiles_UserId",
                table: "Volunteers",
                column: "UserId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Migrants_UserProfiles_UserId",
                table: "Migrants");

            migrationBuilder.DropForeignKey(
                name: "FK_Volunteers_UserProfiles_UserId",
                table: "Volunteers");

            migrationBuilder.DropIndex(
                name: "IX_Volunteers_UserId",
                table: "Volunteers");

            migrationBuilder.DropIndex(
                name: "IX_Migrants_UserId",
                table: "Migrants");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Migrants");

            migrationBuilder.AddColumn<int>(
                name: "MigrantId",
                table: "UserProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VolunteerId",
                table: "UserProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "dc2fd6f2-a223-4157-8672-05e124390108");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "bfe8aef0-9161-40b6-a07a-c3d19b1e2c53");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d370de16-2001-4bb5-b272-6259ac2c2bd3", "AQAAAAEAACcQAAAAENI0ycHPtC2+HCuRmZHQolTHqcD78fZyYN85hWIPqejL1yXTMF+zJ0PnoeyaAIxuNw==" });

            migrationBuilder.InsertData(
                table: "Migrants",
                columns: new[] { "Id", "AmountOfChildren", "FamilyStatus", "Housing", "IsCommonMigrant", "IsEmployed", "IsForcedMigrant", "IsOfficialRefugee" },
                values: new object[] { 1, 0, 0, 0, false, false, false, false });

            migrationBuilder.InsertData(
                table: "Volunteers",
                columns: new[] { "Id", "HasAPlace", "IsATranslator", "IsOrganisation" },
                values: new object[] { 1, false, false, false });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MigrantId", "VolunteerId" },
                values: new object[] { 1, 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Migrants_Id",
                table: "UserProfiles",
                column: "Id",
                principalTable: "Migrants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Volunteers_Id",
                table: "UserProfiles",
                column: "Id",
                principalTable: "Volunteers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
