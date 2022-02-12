using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryManagement.Migrations
{
    public partial class HasFirstBookData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorFirstName", "AuthorLastName", "BookName" },
                values: new object[,]
                {
                    { 1, "Marcel", "Proust", "In Search of Lost Time" },
                    { 2, "James", "Joyce", "Ulysses" },
                    { 3, "Miguel", "de Cervantes", "Don Quixote" },
                    { 4, "Gabriel", "Garcia Marquez", "One Hundred Years of Solitude" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
