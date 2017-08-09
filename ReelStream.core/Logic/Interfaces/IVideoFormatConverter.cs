using ReelStream.data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelStream.core.Logic.Interfaces
{
    public interface IVideoFormatConverter
    {
        void AddVideoToQueue(VideoFile originalVideo);
    }
}
