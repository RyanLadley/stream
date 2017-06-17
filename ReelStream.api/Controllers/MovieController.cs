using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReelStream.api.Models.Repositories.IRepositories;
using ReelStream.api.Models.DataTransfer;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReelStream.api.Controllers
{
    [Route("api/movies")]
    public class MovieController : Controller
    {
        IMovieRepository _repository;

        public MovieController(IMovieRepository repo)
        {
            _repository = repo;
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
    }
}
