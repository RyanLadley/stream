using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.core.External.Models
{
    /// <summary>
    /// This class represents the full response of the external movie db api, including the "useless" information
    /// </summary>
    public class ExternalMovieResponse
    {
        [JsonProperty(PropertyName = "page")]
        public int? Page { get; set; }

        [JsonProperty(PropertyName = "total_results")]
        public int? TotalResults { get; set; }

        [JsonProperty(PropertyName = "total_pages")]
        public int? TotalPages { get; set; }

        [JsonProperty(PropertyName = "results")]
        public List<ExternalMovie> Results { get; set; }
    }
}
