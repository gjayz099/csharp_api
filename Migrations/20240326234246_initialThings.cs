using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_relation.Migrations
{
    /// <inheritdoc />
    public partial class initialThings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Things",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThingsName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Things", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FriendThings",
                columns: table => new
                {
                    FriendsId = table.Column<int>(type: "int", nullable: false),
                    ThingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendThings", x => new { x.FriendsId, x.ThingsId });
                    table.ForeignKey(
                        name: "FK_FriendThings_Things_ThingsId",
                        column: x => x.ThingsId,
                        principalTable: "Things",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FriendThings_friends_FriendsId",
                        column: x => x.FriendsId,
                        principalTable: "friends",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FriendThings_ThingsId",
                table: "FriendThings",
                column: "ThingsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendThings");

            migrationBuilder.DropTable(
                name: "Things");
        }
    }
}
