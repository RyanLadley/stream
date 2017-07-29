using Newtonsoft.Json;
using ReelStream.core.External.Context;
using ReelStream.core.Models.DataTransfer.Response;
using ReelStream.data.Models.Entities;
using ReelStream.data.Models.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.IO;

namespace ReelStream.core.Models.DataTransfer.Form
{
    public class NewMovieForm
    {
        public int? id { get; set; }
        public string title { get; set; }
        public string overview { get; set; }
        public DateTime? releaseDate { get; set; }
        public string posterPath { get; set; }
        public Stream image { get; set; }


        public string genreArrayString { get; set; }

        //Genres are recieved as a string array from the front end. The property will deserialize this on it's first access. 
        private ICollection<GenreForm> _genres;
        public ICollection<GenreForm> genres {
            get
            {
                if(_genres == null || _genres.Count == 0)
                    _genres = JsonConvert.DeserializeObject<ICollection<GenreForm>>(genreArrayString);
                return _genres;
            }
            set  {  _genres = value;  }
        }

        public string imagePath { get { return Path.Combine(new String[]{"wwwroot", "images","movies","11", $"{title.Replace(":", "")}.jpg"}); } }
       
        public void ResolveMovieImage(IExternalMovieDatabase externalDB)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
            if(!String.IsNullOrEmpty(posterPath))
            {
                externalDB.SaveMovieImage(this);
            }
        }
        

        public Movie MapToEntity(VideoFile file, bool includeGenreEntities = false)
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
                    MovieId = movieEntity.MovieId,
                    Movie = movieEntity,
                    GenreId = genre.genreId,
                    Genre = genre.MapToEntity()
                });
            }

            movieEntity.MovieGenres = movieGenres;

            return movieEntity;
        }
    }
}
