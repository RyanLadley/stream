using Microsoft.EntityFrameworkCore;
using ReelStream.data.Models.Context;
using ReelStream.data.Models.Entities;
using ReelStream.data.Models.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.data.Models.Repositories
{
    public class VideoFileRepository : IVideoFileRepository
    {
        MainContext _context;

        public VideoFileRepository(MainContext context)
        {
            _context = context;
        }

        public VideoFile GetFromMovieId(long movieId)
        {
            var file = _context.Movies
                            .Include(movie => movie.VideoFile)
                       .FirstOrDefault(movie => movie.MovieId == movieId)
                       .VideoFile;

            return file;               
        }

        public VideoFile Update(VideoFile videoFile)
        {
            _context.Update(videoFile);
            _context.SaveChanges();

            return videoFile;
        }

        public VideoFile Save(VideoFile videoFile)
        {
            _context.SaveChanges();

            return videoFile;
        }
    }
}
