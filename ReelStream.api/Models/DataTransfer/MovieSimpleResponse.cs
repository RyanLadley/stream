using ReelStream.api.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.api.Models.DataTransfer
{
    public class MovieSimpleResponse
    {
        public long MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; }
        
        public long? VideoFileId { get; set; }

        #region Entity Map
        public static MovieSimpleResponse MapFromEntity(Movie movie) {

            MovieSimpleResponse movieDto = new MovieSimpleResponse()
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                Description = movie.Description,
                Year = movie.Year,
                ImageUrl = movie.ImageUrl,
                VideoFileId = movie.VideoFileId
            };

            return movieDto;
        }
        #endregion

    }
}
