using ReelStream.api.Models.Context.External;
using ReelStream.api.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReelStream.api.Models.DataTransfer
{
    public class NewMovieForm
    {
        public int? id { get; set; }
        public string title { get; set; }
        public string overview { get; set; }
        //public int[] genre_ids { get; set; }
        public DateTime? release_date { get; set; }
        public string poster_path { get; set; }
        public Stream image { get; set; }

        public string imagePath { get { return Path.Combine(new String[]{"wwwroot", "images","movies","11", $"{title}.jpg"}); } }
       
        public void ResolveMovieImage(IExternalMovieDatabase externalDB)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
            if(!String.IsNullOrEmpty(poster_path))
            {
                externalDB.SaveMovieImage(this);
            }
        }

        public Movie MapToEntity(VideoFile file)
        {
            return new Movie
            {
                Title = title,
                Description = overview,
                Year = release_date.HasValue ? release_date.Value.Year : 0,
                ImageUrl = imagePath.Replace("wwwroot\\", "").Replace("\\", "/"),
                VideoFile = file
            };
        }
    }
}
