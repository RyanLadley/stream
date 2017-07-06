using ReelStream.api.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.api.Models.DataTransfer.Form
{
    public class GenreForm
    {
        public int genreId { get; set; }
        public string name { get; set; }

        public Genre MapToEntity()
        {
            var genre = new Genre()
            {
                GenreId = this.genreId,
                Name = this.name
            };

            return genre;
        }
    }
}
