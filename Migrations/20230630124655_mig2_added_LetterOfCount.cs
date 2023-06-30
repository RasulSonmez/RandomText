using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RandomText.Migrations
{
    public partial class mig2_added_LetterOfCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LetterOfCount",
                table: "RandomLetters",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LetterOfCount",
                table: "RandomLetters");
        }
    }
}
