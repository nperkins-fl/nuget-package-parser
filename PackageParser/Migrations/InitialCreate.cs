using Microsoft.EntityFrameworkCore.Migrations;

namespace PackageParser.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PackagesConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Folder = table.Column<string>(nullable: true),
                    FullPath = table.Column<string>(nullable: true),
                    TopLevelFolder = table.Column<string>(nullable: true),
                    SolutionFile = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackagesConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Version = table.Column<string>(nullable: true),
                    PackagesConfigId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Packages_PackagesConfigs_PackagesConfigId",
                        column: x => x.PackagesConfigId,
                        principalTable: "PackagesConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Packages_PackagesConfigId",
                table: "Packages",
                column: "PackagesConfigId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "PackagesConfigs");
        }
    }
}
