using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateSequence(
                name: "productitemseq",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    ProduceDate = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    ManufacturePhone = table.Column<string>(type: "varchar(50)", nullable: false),
                    ManufactureEmail = table.Column<string>(type: "varchar(200)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ManufacturePhone_ProduceDate",
                schema: "dbo",
                table: "Product",
                columns: new[] { "ManufacturePhone", "ProduceDate" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "productitemseq");
        }
    }
}
