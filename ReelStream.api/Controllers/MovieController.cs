using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReelStream.api.Models.Repositories.IRepositories;
using ReelStream.api.Models.DataTransfer;
using System.IO;
using ReelStream.api.Models.Context.External;
using ReelStream.api.Models.DataTransfer.External;
using ReelStream.api.Logic;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReelStream.api.Controllers
{
    [Route("api/movies")]
    public class MovieController : Controller
    {
        IMovieRepository _repository;
        IExternalMovieDatabase _externalDB;
        FileUpload _uploadService;

        public MovieController(IMovieRepository repo, IExternalMovieDatabase external)
        {
            _repository = repo;
            _externalDB = external;
            _uploadService = new FileUpload();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var movies = _repository.GetAll();

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
            List<ExternalMovie> response = await _externalDB.SearchMovie(searchTerm);
            return Ok(response);
        }
    }
}
