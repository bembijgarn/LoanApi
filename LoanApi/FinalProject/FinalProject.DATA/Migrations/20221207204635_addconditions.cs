using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.DATA.Migrations
{
    public partial class addconditions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoanCondition",
                table: "Loan",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoanCondition",
                table: "Loan");
        }
    }
}
