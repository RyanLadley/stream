using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.core.Logic.Interfaces
{
    public interface IMediaManager
    {
        /// <summary>
        /// Runs the ffmpeg_video-converson.bat.bat file that converts the orignal format to the desired, then deletes the original
        /// </summary>
        /// <param name="newVideo">The video format that we will be creating</param>
        /// <returns></returns>
        void VideoConversion(string originalPath, string newPath);


        /// <summary>
        /// Retrives the duration of the provided video in miliseconds
        /// </summary>
        /// <param name="videoPath">The path to the video</param>
        /// <returns>An unsigned long containing the duration in miliseconds</returns>
        long GetVideoDuration(string videoPath);
    }
}
