using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.data.Models.Entities
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public int ExternalId { get; set; } //Currently Maps to TMDB genre id's

        public List<MovieGenre> MovieGenres { get; set; }
    }
}
