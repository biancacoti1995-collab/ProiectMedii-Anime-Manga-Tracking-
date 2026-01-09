using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectMedii_Anime___Manga_Tracking_.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingAndTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StarRating",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "MediaItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTracker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaItemId = table.Column<int>(type: "int", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentProgress = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTracker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTracker_MediaItem_MediaItemId",
                        column: x => x.MediaItemId,
                        principalTable: "MediaItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaItem_CategoryId",
                table: "MediaItem",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTracker_MediaItemId",
                table: "UserTracker",
                column: "MediaItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaItem_Category_CategoryId",
                table: "MediaItem",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaItem_Category_CategoryId",
                table: "MediaItem");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "UserTracker");

            migrationBuilder.DropIndex(
                name: "IX_MediaItem_CategoryId",
                table: "MediaItem");

            migrationBuilder.DropColumn(
                name: "StarRating",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "MediaItem");
        }
    }
}
