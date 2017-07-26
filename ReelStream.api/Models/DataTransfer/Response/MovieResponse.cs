using ReelStream.data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.api.Models.DataTransfer.Response
{
    public class MovieResponse
    {
        public long MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; }
        public double PlaybackTime { get; set; }
        public double? Duration { get; set; }
        public List<GenreResponse> Genres { get; set; }

        #region Entity Map
        public static MovieResponse MapFromEntity(Movie movie) {

            //Create list of GenreResponses from the movies many-many map
            var genres = new List<GenreResponse>();
            foreach(var movieGenre in movie.MovieGenres)
            {
                genres.Add(GenreResponse.MapFromEntity(movieGenre.Genre));
            }
            
            MovieResponse movieDto = new MovieResponse()
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                Description = movie.Description,
                Year = movie.Year,
                ImageUrl = movie.ImageUrl,
                PlaybackTime = (movie.PlaybackTime.HasValue) ? movie.PlaybackTime.Value.TotalSeconds : 0,
                Duration = (movie.VideoFile != null && movie.VideoFile.Duration.HasValue) ? movie.VideoFile.Duration.Value.TotalSeconds : (double?)null,
                Genres = genres
            };

            return movieDto;
        }
        #endregion

    }
}
