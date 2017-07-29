using Microsoft.Extensions.Options;
using System.Net;
using ReelStream.core.Settings;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq; //json.net
using ReelStream.core.Models.DataTransfer;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReelStream.core.External.Models;
using ReelStream.core.Models.DataTransfer.Form;
using System.IO;

namespace ReelStream.core.External.Context
{
    /// <summary>
    /// Used to get external meta-data from an external api
    /// Current Source: The Movie Database (https://developers.themoviedb.org)
    /// </summary>
    public class ExternalMovieDatabase : IExternalMovieDatabase
    {
        private readonly ExternalMovieDBSettings _settings;

        public ExternalMovieDatabase(IOptions<AppSettings> settings)
        {
            _settings = settings.Value.ExternalMovieDBSettings;
        }

        public async Task<List<ExternalMovie>> SearchMovie(string title)
        {
            string apiRequest = $"{_settings.ApiUrl}/search/movie?api_key={_settings.ApiKey}&language=en-US&query={title}&page=1&include_adult=false";

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(apiRequest))
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();

                ExternalMovieResponse resultObject = JsonConvert.DeserializeObject<ExternalMovieResponse>(result);

                List<ExternalMovie> movies = resultObject.Results;

                return movies;
            }
        }

        public async Task SaveMovieImage(NewMovieForm movie)
        {
            string apiRequest = $"{_settings.ImageUrl}{movie.posterPath}";

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(apiRequest))
            using (HttpContent content = response.Content)
            {
                var imageStream = await content.ReadAsStreamAsync();

                using (var fileStream = new FileStream(movie.imagePath, FileMode.Create, FileAccess.Write))
                {
                    imageStream.CopyTo(fileStream);
                }
                    
            }
        }
    }
}
