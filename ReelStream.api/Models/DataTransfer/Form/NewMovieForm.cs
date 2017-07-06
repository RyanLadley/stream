using ReelStream.api.Models.Context.External;
using ReelStream.api.Models.DataTransfer.Response;
using ReelStream.api.Models.Entities;
using ReelStream.api.Models.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.IO;

namespace ReelStream.api.Models.DataTransfer.Form
{
    public class NewMovieForm
    {
        public int? id { get; set; }
        public string title { get; set; }
        public string overview { get; set; }
        public List<GenreForm> genres { get; set; } 
        public DateTime? releaseDate { get; set; }
        public string posterPath { get; set; }
        public Stream image { get; set; }

        public string imagePath { get { return Path.Combine(new String[]{"wwwroot", "images","movies","11", $"{title.Replace(":", "")}.jpg"}); } }
       
        public void ResolveMovieImage(IExternalMovieDatabase externalDB)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
            if(!String.IsNullOrEmpty(posterPath))
            {
                externalDB.SaveMovieImage(this);
            }
        }

        public Movie MapToEntity(VideoFile file)
        {
            var movieEntity = new Movie
            {
                Title = title,
                Description = overview,
                Year = releaseDate.HasValue ? releaseDate.Value.Year : 0,
                ImageUrl = imagePath.Replace("wwwroot\\", "").Replace("\\", "/"),
                VideoFile = file
            };

            //Create the many-many mapping from Movie ot Genre
            List<MovieGenre> movieGenres = new List<MovieGenre>();
            foreach(var genre in genres)
            {
                movieGenres.Add(new MovieGenre()
                {
                    Movie = movieEntity,
                    Genre = genre.MapToEntity()
                });
            }

            movieEntity.MovieGenres = movieGenres;

            return movieEntity;
        }
    }
}
