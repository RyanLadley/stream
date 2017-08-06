using ReelStream.core.Logic.Interfaces;
using ReelStream.data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelStream.core.Models.Buisness.Interfaces
{
    public interface IFileMetadata
    {
        
        string Folder { get; set; }
        string FileName { get; set; }
        long FileSize { get; set; }
        string FileExtension { get; set; }
        long Duration { get; set; }
        string FilePath { get; }


        void GetDuration(IMediaManager mediaManager);
        VideoFile MapToVideoFileEntity();
        VideoFile MapToExistingVideoFileEntity(VideoFile videoFile);

        void VerifyFileUniqueness();
    }
}
