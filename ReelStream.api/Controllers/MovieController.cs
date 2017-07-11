using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReelStream.api.Models.Repositories.IRepositories;
using ReelStream.api.Models.DataTransfer.Response;
using ReelStream.api.Models.Context.External;
using ReelStream.api.Models.DataTransfer.External;
using ReelStream.api.Logic;

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

            List<MovieSimpleResponse> simpleResponse = new List<MovieSimpleResponse>();

            foreach(var movie in movies)
            {
                simpleResponse.Add(MovieSimpleResponse.MapFromEntity(movie));
            }

            return Ok(simpleResponse);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            throw new NotImplementedException();
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

        [HttpGet("genre/{genreId}")]
        public IActionResult GetAllByGenre(int genreId)
        {
            var movies = _movieRepository.GetAllForGenre(genreId);

            List<MovieSimpleResponse> simpleResponse = new List<MovieSimpleResponse>();
            foreach (var movie in movies)
            {
                simpleResponse.Add(MovieSimpleResponse.MapFromEntity(movie));
            }

            return Ok(simpleResponse);
        }
    }
}
