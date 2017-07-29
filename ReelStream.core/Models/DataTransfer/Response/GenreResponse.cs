using ReelStream.data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.core.Models.DataTransfer.Response
{
    public class GenreResponse
    {
        public int GenreId { get; set; }
        public string Name { get; set; }

        public static GenreResponse MapFromObject(Genre genre)
        {
            var genreDto = new GenreResponse()
            {
                GenreId = genre.GenreId,
                Name = genre.Name
            };

            return genreDto;
        }
    }
}
