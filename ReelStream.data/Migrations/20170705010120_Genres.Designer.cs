using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ReelStream.data.Models.Context;

namespace ReelStream.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20170705010120_Genres")]
    partial class Genres
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReelStream.api.Models.Entities.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ExternalId");

                    b.Property<string>("Name");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");
                });

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

            modelBuilder.Entity("ReelStream.api.Models.Entities.MovieGenre", b =>
                {
                    b.Property<long>("MovieId");

                    b.Property<int>("GenreId");

                    b.HasKey("MovieId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("MovieGenres");
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

            modelBuilder.Entity("ReelStream.api.Models.Entities.MovieGenre", b =>
                {
                    b.HasOne("ReelStream.api.Models.Entities.Genre", "Genre")
                        .WithMany("MovieGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReelStream.api.Models.Entities.Movie", "Movie")
                        .WithMany("MovieGenres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
