using Microsoft.EntityFrameworkCore.Migrations;

namespace XanAuto.Domain.Migrations
{
    public partial class LoginRegisterPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AdminPermit",
                schema: "Membership",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminPermit",
                schema: "Membership",
                table: "Users");
        }
    }
}
