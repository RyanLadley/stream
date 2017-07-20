using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReelStream.api.Models.Repositories.IRepositories;
using ReelStream.api.Models.DataTransfer.Response;
using ReelStream.api.Models.Context.External;
using ReelStream.api.Models.DataTransfer.External;
using ReelStream.api.Logic;
using ReelStream.api.Models.DataTransfer.Form;
using System.IO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReelStream.api.Controllers
{
    [Route("api/movies")]
    public class MovieController : Controller
    {
        IMovieRepository _movieRepository;
        IExternalMovieDatabase _externalDB;
        IGenreRepository _genreRepository;
        FileUpload _uploadService;

        public MovieController(IMovieRepository movieRepo, IGenreRepository genreRepo, IExternalMovieDatabase external)
        {
            _movieRepository = movieRepo;
            _genreRepository = genreRepo;
            _externalDB = external;
            _uploadService = new FileUpload();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var movies = _movieRepository.GetAll();

            List<MovieResponse> simpleResponse = new List<MovieResponse>();

            foreach(var movie in movies)
            {
                simpleResponse.Add(MovieResponse.MapFromEntity(movie));
            }

            return Ok(simpleResponse);
        }

        [HttpGet("{movieId}")]
        public IActionResult Get(long movieId)
        {
            var movies = _movieRepository.Get(movieId);

            return Ok(MovieResponse.MapFromEntity(movies));
        }
        
        [HttpGet("search/{searchTerm}")]
        public async Task<IActionResult> SearchMovie(string searchTerm)
       {
            List<ExternalMovie> externalMovie = await _externalDB.SearchMovie(searchTerm);

            List<MovieSearchResponse> response = new List<MovieSearchResponse>();
            foreach(var movie in externalMovie)
            {
                response.Add(MovieSearchResponse.MapFromEntity(movie, _genreRepository));
            }

            return Ok(response);
        }

        [HttpPost("playback/{movieId}")]
        public IActionResult UpdatePlayback(int movieId, [FromBody] UpdateMoviePlaybackForm moviePlayback)
        {
            string responseBody = new StreamReader(Request.Body).ReadToEnd();
            _movieRepository.UpdatePlayback(moviePlayback.MapToEntity());

            return NoContent();
        }

        [HttpGet("genre/{genreId}")]
        public IActionResult GetAllByGenre(int genreId)
        {
            var movies = _movieRepository.GetAllForGenre(genreId);

            List<MovieResponse> simpleResponse = new List<MovieResponse>();
            foreach (var movie in movies)
            {
                simpleResponse.Add(MovieResponse.MapFromEntity(movie));
            }

            return Ok(simpleResponse);
        }
    }
}
