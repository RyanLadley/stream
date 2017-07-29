using ReelStream.data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.core.Models.DataTransfer.Form
{
    public class UpdateMoviePlaybackForm
    {
        public long movieId { get; set; }
        public double playbackTime { get; set; } //In Seconds

        public Movie MapToEntity()
        {
            var movieEntity = new Movie
            {
                MovieId = movieId,
                PlaybackTime = TimeSpan.FromSeconds(playbackTime)
            };

            return movieEntity;
        }
    }
}
