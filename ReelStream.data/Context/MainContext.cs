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
           
            modelBuilder = _buildMovie(modelBuilder);
            modelBuilder = _buildVideoFile(modelBuilder);
            modelBuilder = _buildMovieGenre(modelBuilder);
            modelBuilder = _buildUser(modelBuilder);

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<VideoFile> VideoFiles { get; set; }
        public DbSet<User> Users { get; set; }

        private ModelBuilder _buildMovie(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Movie>()
                .HasOne(mg => mg.User);

            modelBuilder.Entity<Movie>()
               .Property(movie => movie.Adult)
                   .HasDefaultValue(0);

            modelBuilder.Entity<Movie>()
               .Property(movie => movie.Title).IsRequired();

            return modelBuilder;
        }

        private ModelBuilder _buildVideoFile(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VideoFile>()
                .Property(video => video.Folder).IsRequired();

            modelBuilder.Entity<VideoFile>()
                .Property(video => video.FileName).IsRequired();

            modelBuilder.Entity<VideoFile>()
                .Property(video => video.FileExtension).IsRequired();
            
            return modelBuilder;
        }
        
        private ModelBuilder _buildUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(user => user.Username)
                    .IsUnique();

            modelBuilder.Entity<User>()
                .Property(user => user.Username).IsRequired();

            modelBuilder.Entity<User>()
                .Property(user => user.Email).IsRequired();

            modelBuilder.Entity<User>()
                .Property(user => user.Password).IsRequired();

            modelBuilder.Entity<User>()
                .Property(user => user.Salt).IsRequired();

            modelBuilder.Entity<User>()
                .Property(user => user.FirstName).IsRequired();

            modelBuilder.Entity<User>()
                .Property(user => user.LastName).IsRequired();

            return modelBuilder;
        }
        
        private ModelBuilder _buildMovieGenre(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(mg => mg.MovieId);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => mg.GenreId);

            return modelBuilder;
        }
    }
}
