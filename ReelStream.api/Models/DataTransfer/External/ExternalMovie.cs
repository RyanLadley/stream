using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ReelStream.api.Models.DataTransfer.External
{
    /// <summary>
    /// This class represents the the indivdual movie resultes from the external movie db api call
    /// </summary>
    public class ExternalMovie
    {
        [JsonProperty(PropertyName = "id")]
        public int? ExternalId { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string title { get; set; }

        [JsonProperty(PropertyName = "poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty(PropertyName = "genre_ids")]
        public int[] GenreIds { get; set; }

        [JsonProperty(PropertyName = "adult")]
        public bool? Adult { get; set; }

        [JsonProperty(PropertyName = "overview")]
        public string Overview { get; set; }

        [JsonProperty(PropertyName = "release_date")]
        public DateTime? ReleaseDate { get; set; }
    }
}
