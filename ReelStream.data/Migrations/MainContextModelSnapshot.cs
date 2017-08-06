using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ReelStream.data.Models.Context;

namespace ReelStream.data.Migrations
{
    [DbContext(typeof(MainContext))]
    partial class MainContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReelStream.data.Models.Entities.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ExternalId");

                    b.Property<string>("Name");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("ReelStream.data.Models.Entities.Movie", b =>
                {
                    b.Property<long>("MovieId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Adult")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<DateTime?>("LastViewDate");

                    b.Property<TimeSpan?>("PlaybackTime");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<long>("UserId");

                    b.Property<long>("VideoFileId");

                    b.Property<int>("Year");

                    b.HasKey("MovieId");

                    b.HasIndex("UserId");

                    b.HasIndex("VideoFileId")
                        .IsUnique();

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("ReelStream.data.Models.Entities.MovieGenre", b =>
                {
                    b.Property<long>("MovieId");

                    b.Property<int>("GenreId");

                    b.HasKey("MovieId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("MovieGenres");
                });

            modelBuilder.Entity("ReelStream.data.Models.Entities.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<byte[]>("Salt")
                        .IsRequired();

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ReelStream.data.Models.Entities.VideoFile", b =>
                {
                    b.Property<long>("VideoFileId")
                        .ValueGeneratedOnAdd();

                    b.Property<TimeSpan?>("Duration");

                    b.Property<string>("FileExtension")
                        .IsRequired();

                    b.Property<string>("FileName")
                        .IsRequired();

                    b.Property<string>("Folder")
                        .IsRequired();

                    b.HasKey("VideoFileId");

                    b.ToTable("VideoFiles");
                });

            modelBuilder.Entity("ReelStream.data.Models.Entities.Movie", b =>
                {
                    b.HasOne("ReelStream.data.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReelStream.data.Models.Entities.VideoFile", "VideoFile")
                        .WithOne("Movie")
                        .HasForeignKey("ReelStream.data.Models.Entities.Movie", "VideoFileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReelStream.data.Models.Entities.MovieGenre", b =>
                {
                    b.HasOne("ReelStream.data.Models.Entities.Genre", "Genre")
                        .WithMany("MovieGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReelStream.data.Models.Entities.Movie", "Movie")
                        .WithMany("MovieGenres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
