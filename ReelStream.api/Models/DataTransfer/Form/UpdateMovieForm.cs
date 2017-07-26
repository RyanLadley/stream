using ReelStream.api.Models.DataTransfer.Response;
using ReelStream.data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.api.Models.DataTransfer.Form
{
    public class UpdateMovieForm
    {
        public long movieId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int year { get; set; }
        public string imageUrl { get; set; }
        public double playbackTime { get; set; }
        public string genreIds { get; set; }

        public Movie MapToEntity()
        {
            var movieEntity = new Movie
            {
                MovieId = movieId,
                Title = title,
                Description = description,
                Year = year,
                ImageUrl = imageUrl,
                PlaybackTime = TimeSpan.FromSeconds(playbackTime)
            };

            //Create the many-many mapping from Movie ot Genre
            List<MovieGenre> movieGenres = new List<MovieGenre>();
            foreach (var genreId in genreIds.Split(',')) 
            {
                movieGenres.Add(new MovieGenre()
                {
                    MovieId = movieEntity.MovieId,
                    GenreId = Int32.Parse(genreId)
                });
            }

            movieEntity.MovieGenres = movieGenres;

            return movieEntity;
        }
    }
}
