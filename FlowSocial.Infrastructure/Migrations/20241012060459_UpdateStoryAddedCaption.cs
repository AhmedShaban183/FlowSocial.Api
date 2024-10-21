using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowSocial.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStoryAddedCaption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Caption",
                table: "Stories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caption",
                table: "Stories");
        }
    }
}
