using Microsoft.AspNetCore.Mvc;
using ReelStream.api.Models.Repositories.IRepositories;
using ReelStream.api.Logic;
using Microsoft.Net.Http.Headers;
using System.IO;

namespace ReelStream.api.Controllers
{

    [Route("api/video")]
    public class VideoController : Controller
    {

        IVideoFileRepository _repository;

        public VideoController(IVideoFileRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{movieId}")]
        public IActionResult GetFromMovie(long movieId)
        {
            var videoFile = _repository.GetFromMovieId(movieId);

            var filename = $"wwwroot/{videoFile.Folder}/{videoFile.FileName}.{videoFile.FileExtension}";
            VideoStream stream = new VideoStream(new FileStream(filename, FileMode.Open, FileAccess.Read),
                                                 new MediaTypeHeaderValue($"video/{videoFile.FileExtension}"));
            
            return stream;
        }
    }
}
