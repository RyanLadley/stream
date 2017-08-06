using Microsoft.AspNetCore.Mvc;
using ReelStream.core.Logic;
using ReelStream.core.Models.Buisness;
using ReelStream.core.External.Context;
using ReelStream.core.Models.DataTransfer.Form;
using ReelStream.core.Models.DataTransfer.Response;
using ReelStream.data.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ReelStream.auth.Logic;

namespace ReelStream.api.Controllers
{
    [Route("api/upload")]
    public class UploadController : Controller
    {
        FileUpload _uploadService;
        private readonly IExternalMovieDatabase _externalDB;
        IMovieRepository _movieRpository;
        IVideoFileRepository _videoFileRpository;

        public UploadController(IMovieRepository movieRepo, IVideoFileRepository videoFileRepo, IGenreRepository genreRepo, IExternalMovieDatabase external)
        {
            _uploadService = new FileUpload();
            _externalDB = external;
            _movieRpository = movieRepo;
            _videoFileRpository = videoFileRepo;
        }


        [HttpGet("movie")]
        [Authorize(Policy = "GeneralUser")]
        public IActionResult TestMovieUpload(int flowChunkNumber, string flowIdentifier)
        {
            var userId = TokenManager.ExtractUserId(User.Claims);
            if (_uploadService.ChunkHasArrived(userId, flowChunkNumber, flowIdentifier))
                return Ok();
            else
                return NoContent();

        }

        [HttpPost("movie")]
        [Authorize(Policy = "GeneralUser")]
        public async Task<IActionResult> MovieUpload(FlowUploadForm flow, NewMovieForm newMovie)
        {
            var userId = TokenManager.ExtractUserId(User.Claims);
            try
            {
                foreach (var formFile in Request.Form.Files)
                {
                    using (var readStream = formFile.OpenReadStream())
                    using (var binaryReader = new BinaryReader(readStream))
                    {
                        var fileContent = binaryReader.ReadBytes((int)formFile.Length);
                        await _uploadService.AddChunkFile(userId, fileContent, flow);
                    }
                    if(_uploadService.AttemptCompleteFileCreation(userId, flow, formFile.FileName, out FileMetadata metadata))
                    {
                        newMovie.ResolveMovieImage(_externalDB);
                        metadata.GetDuration(new MediaManager());

                        var movieEntity = newMovie.MapToEntity(userId, metadata.MapToVideoFileEntity());
                        movieEntity.DateCreated = DateTime.Now;
                        movieEntity = _movieRpository.Add(movieEntity);
                        
                        //If file is not mp4. it cannot be streamed properly to a browser so convert it
                        //This process takes a while. Consider moveing this out of the flow
                        if (metadata.FileExtension != ".mp4")
                        {
                            VideoFormatConverter converter = new VideoFormatConverter(metadata, new MediaManager());
                            metadata = converter.ConvertTo("mp4");
                            metadata.GetDuration(new MediaManager());

                            var videoFileEntity = metadata.MapToExistingVideoFileEntity(movieEntity.VideoFile);
                            //This saves the pending changes to the db. If this block is moved, change Save to Update. 
                            _videoFileRpository.Update(videoFileEntity);
                            movieEntity.VideoFile = videoFileEntity;
                        }


                        return Created($"api/movies/{movieEntity.MovieId}", MovieResponse.MapFromObject(movieEntity));
                    }
                }

            }
            catch 
            {
                return StatusCode(500, "There was an Error Uploading the File");
            }
            return Ok();
        }
    }
}
