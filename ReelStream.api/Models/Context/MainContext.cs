using Microsoft.EntityFrameworkCore;
using ReelStream.api.Models.Entities;

namespace ReelStream.api.Models.Context
{
    public class MainContext : DbContext
    {

        public MainContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Movie>().Property(m => m.VideoFile).IsRequired();

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<VideoFile> VideoFiles { get; set; }
    }
}
