using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReelStream.data.Repositories.IRepositories;
using ReelStream.core.Models.DataTransfer.Response;
using ReelStream.core.External.Context;
using ReelStream.core.External.Models;
using ReelStream.core.Logic;
using ReelStream.core.Models.DataTransfer.Form;
using System.IO;
using ReelStream.auth.Logic;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Policy = "GeneralUser")]
        public IActionResult GetAll()
        {
            var userId = TokenManager.ExtractUserId(User.Claims);
            var movies = _movieRepository.GetAll(userId);

            List<MovieResponse> simpleResponse = new List<MovieResponse>();

            foreach(var movie in movies)
            {
                simpleResponse.Add(MovieResponse.MapFromObject(movie));
            }

            return Ok(simpleResponse);
        }

        [HttpGet("queues")]
        [Authorize(Policy = "GeneralUser")]
        public IActionResult GetQueues()
        {
            var userId = TokenManager.ExtractUserId(User.Claims);
            QueueCurator curator = new QueueCurator(_movieRepository);
            var queues = curator.BuildMovieQueues(userId);

            List<MovieQueueResponse> responseQueues = new List<MovieQueueResponse>();

            foreach (var queue in queues)
            {
                responseQueues.Add(MovieQueueResponse.MapFromObject(queue));
            }

            return Ok(responseQueues);
        }


        [HttpGet("{movieId}")]
        public IActionResult Get(long movieId)
        {
            var movies = _movieRepository.Get(movieId);

            return Ok(MovieResponse.MapFromObject(movies));
        }
        
        [HttpGet("search/{searchTerm}")]
        [Authorize(Policy = "GeneralUser")]
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
        [Authorize(Policy = "GeneralUser")]
        public IActionResult UpdatePlayback(int movieId, [FromBody] UpdateMoviePlaybackForm moviePlayback)
        {
            string responseBody = new StreamReader(Request.Body).ReadToEnd();
            _movieRepository.UpdatePlayback(moviePlayback.MapToEntity());

            return NoContent();
        }

        [HttpGet("genre/{genreId}")]
        [Authorize(Policy = "GeneralUser")]
        public IActionResult GetAllByGenre(int genreId)
        {
            var userId = TokenManager.ExtractUserId(User.Claims);

            var movies = _movieRepository.GetAllForGenre(userId, genreId);

            List<MovieResponse> simpleResponse = new List<MovieResponse>();
            foreach (var movie in movies)
            {
                simpleResponse.Add(MovieResponse.MapFromObject(movie));
            }

            return Ok(simpleResponse);
        }
    }
}
