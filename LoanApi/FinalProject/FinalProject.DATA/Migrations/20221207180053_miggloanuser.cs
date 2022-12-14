using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.DATA.Migrations
{
    public partial class miggloanuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Loan",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "LoanPeriod",
                table: "Loan",
                newName: "LoanPeriodmonthly");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_UserId",
                table: "Loan",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_User_UserId",
                table: "Loan",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loan_User_UserId",
                table: "Loan");

            migrationBuilder.DropIndex(
                name: "IX_Loan_UserId",
                table: "Loan");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Loan",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "LoanPeriodmonthly",
                table: "Loan",
                newName: "LoanPeriod");
        }
    }
}
