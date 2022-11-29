using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoadBalancer.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Balancer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Listeners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Port = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    Load = table.Column<int>(type: "int", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listeners", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Listeners");
        }
    }
}
