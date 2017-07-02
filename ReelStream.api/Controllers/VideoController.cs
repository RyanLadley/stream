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

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var videoFile = _repository.Get(id);

            var filename = $"wwwroot/video/{videoFile.Folder}/{videoFile.FileName}.{videoFile.FileExtension}";
            VideoStream stream = new VideoStream(new FileStream(filename, FileMode.Open, FileAccess.Read),
                                                 new MediaTypeHeaderValue($"video/{videoFile.FileExtension}"));
            
            return stream;
        }
    }
}
