using ReelStream.api.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.api.Models.DataTransfer.Response
{
    public class MovieSimpleResponse
    {
        public long MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; }
        public List<GenreResponse> Genres { get; set; }

        public long? VideoFileId { get; set; }

        #region Entity Map
        public static MovieSimpleResponse MapFromEntity(Movie movie) {

            //Create list of GenreResponses from the movies many-many map
            var genres = new List<GenreResponse>();
            foreach(var movieGenre in movie.MovieGenres)
            {
                genres.Add(GenreResponse.MapFromEntity(movieGenre.Genre));
            }

            MovieSimpleResponse movieDto = new MovieSimpleResponse()
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                Description = movie.Description,
                Year = movie.Year,
                ImageUrl = movie.ImageUrl,
                VideoFileId = movie.VideoFileId,
                Genres = genres
            };

            return movieDto;
        }
        #endregion

    }
}
