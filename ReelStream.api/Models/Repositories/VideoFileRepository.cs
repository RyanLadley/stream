using ReelStream.api.Models.Context;
using ReelStream.api.Models.Entities;
using ReelStream.api.Models.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.api.Models.Repositories
{
    public class VideoFileRepository : IVideoFileRepository
    {
        MainContext _context;

        public VideoFileRepository(MainContext context)
        {
            _context = context;
        }

        public VideoFile Get(long id)
        {
            return _context.VideoFiles.FirstOrDefault(file => file.VideoFileId == id);
        }
    }
}
