using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace run_script.Migrations
{
    /// <inheritdoc />
    public partial class initscript1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scripts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScriptContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimesAccessed = table.Column<int>(type: "int", nullable: false),
                    LifeStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scripts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Scripts",
                columns: new[] { "Id", "LifeStatus", "Name", "ScriptContent", "TimesAccessed" },
                values: new object[,]
                {
                    { 1, false, "Hello World", "echo \"Hello\" ", 0 },
                    { 2, false, "faiz", "docker start nginxfaiz", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scripts");
        }
    }
}
