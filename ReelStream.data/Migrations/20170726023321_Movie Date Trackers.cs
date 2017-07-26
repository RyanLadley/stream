using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReelStream.Migrations
{
    public partial class MovieDateTrackers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Movies",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastViewDate",
                table: "Movies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "LastViewDate",
                table: "Movies");
        }
    }
}
