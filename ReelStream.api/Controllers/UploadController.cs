using Microsoft.AspNetCore.Mvc;
using ReelStream.api.Logic;
using ReelStream.api.Models.Buisness;
using ReelStream.api.Models.Context.External;
using ReelStream.api.Models.DataTransfer.Form;
using ReelStream.api.Models.DataTransfer.Response;
using ReelStream.api.Models.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult TestMovieUpload(int flowChunkNumber, string flowIdentifier)
        {
            if (_uploadService.ChunkHasArrived(flowChunkNumber, flowIdentifier))
                return Ok();
            else
                return NoContent();

        }

        [HttpPost("movie")]
        public async Task<IActionResult> MovieUpload(FlowUploadForm flow, NewMovieForm newMovie)
        {
            try
            {
                foreach (var formFile in Request.Form.Files)
                {
                    using (var readStream = formFile.OpenReadStream())
                    using (var binaryReader = new BinaryReader(readStream))
                    {
                        var fileContent = binaryReader.ReadBytes((int)formFile.Length);
                        await _uploadService.AddChunkFile(fileContent, flow);
                    }
                    if(_uploadService.AttemptCompleteFileCreation(flow, formFile.FileName, out FileMetadata metadata))
                    {
                        newMovie.ResolveMovieImage(_externalDB);

                        var movieEntity = newMovie.MapToEntity(metadata.MapToVideoFileEntity());
                        movieEntity = _movieRpository.Add(movieEntity);
                        
                        //If file is not mp4. it cannot be streamed properly to a browser so convert it
                        //This process takes a while. Consider moveing this out of the flow
                        if (metadata.FileExtension != ".mp4")
                        {
                            VideoFormatConverter converter = new VideoFormatConverter(metadata);
                            metadata = converter.ConvertTo("mp4");
                            var videoFileEntity = metadata.MapToExistingVideoFileEntity(movieEntity.VideoFile);
                            //This saves the pending changes to the db. If this block is moved, change Save to Update. 
                            _videoFileRpository.Update(videoFileEntity);
                            movieEntity.VideoFile = videoFileEntity;
                        }

                        return Created($"api/movies/{movieEntity.MovieId}", MovieSimpleResponse.MapFromEntity(movieEntity));
                    }
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, "There was an Error Uploading the File");
            }
            return Ok();
        }
    }
}
