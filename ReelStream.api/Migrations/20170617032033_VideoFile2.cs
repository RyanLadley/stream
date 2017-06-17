using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReelStream.Migrations
{
    public partial class VideoFile2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoFiles_Movies_MovieId",
                table: "VideoFiles");

            migrationBuilder.DropIndex(
                name: "IX_VideoFiles_MovieId",
                table: "VideoFiles");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "VideoFiles");

            migrationBuilder.AddColumn<long>(
                name: "VideoFileId",
                table: "Movies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_VideoFileId",
                table: "Movies",
                column: "VideoFileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_VideoFiles_VideoFileId",
                table: "Movies",
                column: "VideoFileId",
                principalTable: "VideoFiles",
                principalColumn: "VideoFileId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_VideoFiles_VideoFileId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_VideoFileId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "VideoFileId",
                table: "Movies");

            migrationBuilder.AddColumn<long>(
                name: "MovieId",
                table: "VideoFiles",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_VideoFiles_MovieId",
                table: "VideoFiles",
                column: "MovieId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoFiles_Movies_MovieId",
                table: "VideoFiles",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
