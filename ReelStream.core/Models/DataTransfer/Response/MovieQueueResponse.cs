using ReelStream.core.Models.Buisness;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelStream.core.Models.DataTransfer.Response
{
    public class MovieQueueResponse
    {
        public string Name { get; set; }
        public List<MovieResponse> Movies { get; set; }

        #region Object Maps
        public static MovieQueueResponse MapFromObject(MovieQueue obj)
        {
            var movieQueueDto = new MovieQueueResponse()
            {
                Name = obj.Name
            };

            movieQueueDto.Movies = new List<MovieResponse>();
            foreach(var movie in obj.Movies)
            {
                movieQueueDto.Movies.Add(MovieResponse.MapFromObject(movie));
            }

            return movieQueueDto;
        }
        #endregion
    }
}
