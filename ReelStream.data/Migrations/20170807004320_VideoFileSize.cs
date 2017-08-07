using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReelStream.data.Migrations
{
    public partial class VideoFileSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Duration",
                table: "VideoFiles",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FileSize",
                table: "VideoFiles",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "VideoFiles");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Duration",
                table: "VideoFiles",
                nullable: true,
                oldClrType: typeof(TimeSpan));
        }
    }
}
