using Microsoft.AspNetCore.Mvc;
using ReelStream.api.Models.DataTransfer.Response;
using ReelStream.data.Models.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.api.Controllers
{
    [Route("api/genres")]
    public class GenreController : Controller
    {

        IGenreRepository _repository;

        public GenreController(IGenreRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetImplementedByUser()
        {
            var genres = _repository.GetImplementedByUser();

            List<GenreResponse> response = new List<GenreResponse>();
            foreach( var genre in genres)
            {
                response.Add(GenreResponse.MapFromEntity(genre));
            }

            return Ok(response);
        }

        [HttpGet("{genreId}")]
        public IActionResult GetFromId(int genreId)
        {
            var genre = _repository.GetFromId(genreId);

            GenreResponse response = GenreResponse.MapFromEntity(genre);
    
            return Ok(response);
        }
    }
}
