using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsNew.Migrations
{
    /// <inheritdoc />
    public partial class Genreaddedtostudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Genre",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Students");
        }
    }
}
