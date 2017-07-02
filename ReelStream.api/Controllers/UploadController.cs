using Microsoft.AspNetCore.Mvc;
using ReelStream.api.Logic;
using ReelStream.api.Models.Buisness;
using ReelStream.api.Models.Context.External;
using ReelStream.api.Models.DataTransfer;
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

        public UploadController(IMovieRepository movieRepo, IExternalMovieDatabase external)
        {
            _uploadService = new FileUpload();
            _externalDB = external;
            _movieRpository = movieRepo;
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
                        _movieRpository.Add(movieEntity);
                        break;
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
