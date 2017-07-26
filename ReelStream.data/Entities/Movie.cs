using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.data.Models.Entities
{
    public class Movie
    {
        public long MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; }
        public TimeSpan? PlaybackTime { get; set; }
        public DateTime? LastViewDate { get; set; }
        public DateTime? DateCreated { get; set; }

        //TODO: Remove Nullable - requires emtpy db
        public long? VideoFileId { get; set; }
        public VideoFile VideoFile { get; set; }

        public List<MovieGenre> MovieGenres { get; set; }

    }
}
