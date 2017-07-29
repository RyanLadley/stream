using Microsoft.EntityFrameworkCore;
using ReelStream.data.Models.Entities;

namespace ReelStream.data.Models.Context
{
    public class MainContext : DbContext
    {

        public MainContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .Property(movie => movie.Adult)
                    .HasDefaultValue(0);

            modelBuilder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId});

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(mg => mg.MovieId);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => mg.GenreId);

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<VideoFile> VideoFiles { get; set; }
    }
}
