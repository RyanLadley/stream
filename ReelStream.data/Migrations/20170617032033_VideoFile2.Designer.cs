using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ReelStream.data.Models.Context;

namespace ReelStream.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20170617032033_VideoFile2")]
    partial class VideoFile2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReelStream.api.Models.Entities.Movie", b =>
                {
                    b.Property<long>("MovieId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Title");

                    b.Property<long?>("VideoFileId");

                    b.Property<int>("Year");

                    b.HasKey("MovieId");

                    b.HasIndex("VideoFileId")
                        .IsUnique();

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("ReelStream.api.Models.Entities.VideoFile", b =>
                {
                    b.Property<long>("VideoFileId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileExtension");

                    b.Property<string>("FileName");

                    b.Property<string>("Folder");

                    b.HasKey("VideoFileId");

                    b.ToTable("VideoFiles");
                });

            modelBuilder.Entity("ReelStream.api.Models.Entities.Movie", b =>
                {
                    b.HasOne("ReelStream.api.Models.Entities.VideoFile", "VideoFile")
                        .WithOne("Movie")
                        .HasForeignKey("ReelStream.api.Models.Entities.Movie", "VideoFileId");
                });
        }
    }
}
