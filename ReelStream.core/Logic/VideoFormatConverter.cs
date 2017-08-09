using ReelStream.core.Logic.Interfaces;
using ReelStream.core.Models.Buisness;
using ReelStream.data.Models.Entities;
using ReelStream.data.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.core.Logic
{
    public class VideoFormatConverter : IVideoFormatConverter
    {
        private readonly int TOTAL_THREADS = 4;
        private int ThreadsAvailable;
        private string _baseFolder = "wwwroot";

        private IMediaManager _mediaManager;
        private IVideoFileRepository _videoFileReposotroy;

        private Queue<VideoFile> _convertionQueue;
        
        public VideoFormatConverter(IMediaManager mediaManager, IVideoFileRepository videoRepo)
        {
            _mediaManager = mediaManager;
            _videoFileReposotroy = videoRepo;
            
            _convertionQueue = new Queue<VideoFile>();
            ThreadsAvailable = TOTAL_THREADS;
        }

        /// <summary>
        /// Public interface to add files that need to be converted. 
        /// When a thread becomes available, this file will be removed from the queue and converted
        /// </summary>
        /// <param name="originalVideo"></param>
        public void AddVideoToQueue(VideoFile originalVideo)
        {
            _convertionQueue.Enqueue(originalVideo);
            _runConversionIfThreadAvailable();
        }

        /// <summary>
        /// If threads are available, run conversion; When the thread finishes, this method is recursivly called until queue is emtpy
        /// </summary>
        private void _runConversionIfThreadAvailable()
        {
            if(ThreadsAvailable > 0 && _convertionQueue.Count != 0)
            {
                ThreadsAvailable--;
                Task.Run(() => _beginConversion(_convertionQueue.Dequeue(), "mp4"))
                    .ContinueWith((task) => ThreadsAvailable++)
                    .ContinueWith((task) => _runConversionIfThreadAvailable());
            }
        }

        private void _beginConversion(VideoFile originalFile, string extension)
        {
            var originalPath = Path.Combine(_baseFolder, originalFile.Folder, $"{originalFile.FileName}.{originalFile.FileExtension}");
            var newPath = FileMetadata.VerifyFileUniqueness(Path.Combine(_baseFolder, originalFile.Folder, $"{originalFile.FileName}.{extension}"));
            
            try
            {
                _mediaManager.VideoConversion(originalPath, newPath);

                var metadata = new FileMetadata(newPath);
                metadata.GetDuration(_mediaManager);

                var newVideoFile = metadata.MapToExistingVideoFileEntity(originalFile);
                _videoFileReposotroy.Update(newVideoFile);
            }
            catch
            {
                Console.Write("There Was An Error");
            }
        }
        
    }
}
