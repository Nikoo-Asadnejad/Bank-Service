using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankMicroservice.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankTransactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSuccessfull = table.Column<bool>(type: "bit", nullable: true),
                    TransactionDate = table.Column<long>(type: "bigint", nullable: false),
                    BankToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankResult = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankTransactions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankTransactions");
        }
    }
}
