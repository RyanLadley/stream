using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReelStream.api.Models.Repositories.IRepositories;
using ReelStream.api.Logic;
using System.Net.Http;
using Microsoft.Net.Http.Headers;
using System.IO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public Task Get(long id)
        {
            var videoFile = _repository.Get(id);
            HttpContext.Response.ContentType = $"video/{videoFile.FileExtension}";
            var videoStream = new VideoStream(videoFile);
            return videoStream.GetStream().CopyToAsync(HttpContext.Response.Body);

        }
    }
}
