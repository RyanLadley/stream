using ReelStream.api.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.api.Models.Repositories.IRepositories
{
    public interface IVideoFileRepository
    {
        VideoFile GetFromMovieId(long movieId);
        VideoFile Update(VideoFile videoFile);
        VideoFile Save(VideoFile videoFile);
    }
}
