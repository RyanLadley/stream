using System;
using System.Collections.Generic;
using ReelStream.api.Models.Entities;
using ReelStream.api.Models.Repositories.IRepositories;
using ReelStream.api.Models.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data.SqlClient;

namespace ReelStream.api.Models.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private MainContext _context;

        #region SQL
        //Returns a list of movies that contain the genre provided in the @genreId param
        private string sqlSelectMoviesForGenre =
                $"SELECT Movies.* "+
                $"  FROM Movies " +
                $"  JOIN MovieGenres on MovieGenres.MovieId = Movies.MovieId " +
                $"  WHERE MovieGenres.GenreId = @genreId";


        private string sqlUpdateMoviePlaybackTime =
            $"UPDATE Movies " +
            $"  SET PlaybackTime = @playbackTime " +
            $"WHERE MovieId = @movieId; ";
        #endregion

        public MovieRepository(MainContext context)
        {
            
            _context = context;
        }
        public Movie Add(Movie movie)
        {
            //We only care about updating the Id's from the MovieGenre entity, so we ignore the actual objects.
            List<Genre> ignoredFields = new List<Genre>(); //Keeps track of the element so we can track it again after the insert.
            for(var i = 0; i < movie.MovieGenres.Count; i++)
            {
                ignoredFields.Insert(i, movie.MovieGenres.ElementAt(i).Genre);
                movie.MovieGenres.ElementAt(i).Genre = null;
            }

            _context.Add(movie);
            _context.SaveChanges();

            //Reset the objects to being tracked incase another step wants to use it.
            for (var i = 0; i < movie.MovieGenres.Count; i++)
            {
                movie.MovieGenres.ElementAt(i).Genre = ignoredFields.ElementAt(i);
                _context.Entry(movie.MovieGenres.ElementAt(i).Genre).State = EntityState.Unchanged;
                _context.Entry(movie.MovieGenres.ElementAt(i)).State = EntityState.Unchanged;
            }

            return movie;
        }

        public Movie Get(long movieId)
        {
            return _context.Movies
                    .Include(movie => movie.MovieGenres)
                        .ThenInclude(mg => mg.Genre)
                    .FirstOrDefault(movie => movie.MovieId == movieId);
        }

        public List<Movie> GetAll()
        {
            return _context.Movies
                .Include(movie => movie.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(movie => movie.VideoFile)
                .ToList();
        }

        public List<Movie> GetAllForGenre(int genreId)
        {
            var pGenreId = new SqlParameter("@genreId", genreId);
            var movies = _context.Movies.FromSql(sqlSelectMoviesForGenre, pGenreId)
                .Include(movie => movie.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(movie => movie.VideoFile)
                .ToList();

            return movies;
        }

        public Movie GetFromVideoId(long id)
        {
            var movie = _context.Movies.FirstOrDefault(queryMovie => queryMovie.VideoFileId == id);

            return movie;
        }

        public void Remove(long id)
        {
            throw new NotImplementedException();
        }

        public Movie UpdatePlayback(Movie movie)
        {
            var pMovieId = new SqlParameter("@movieId", movie.MovieId);
            var pPlayybackTime = new SqlParameter("@playbackTime", movie.PlaybackTime);
            _context.Database.ExecuteSqlCommand(sqlUpdateMoviePlaybackTime, pPlayybackTime, pMovieId);

            return movie;
        }
    }
}
