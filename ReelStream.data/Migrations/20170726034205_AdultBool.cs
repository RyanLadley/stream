using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReelStream.Migrations
{
    public partial class AdultBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Movies",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Adult",
                table: "Movies",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adult",
                table: "Movies");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Movies",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
