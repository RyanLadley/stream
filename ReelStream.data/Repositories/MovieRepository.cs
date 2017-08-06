using System;
using System.Collections.Generic;
using ReelStream.data.Models.Entities;
using ReelStream.data.Repositories.IRepositories;
using ReelStream.data.Models.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data.SqlClient;

namespace ReelStream.data.Repositories
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
                $"  WHERE Movies.UserId = @userId" +
                $"      AND MovieGenres.GenreId = @genreId";

        private string sqlSelectMoviesInProgress =
                $"SELECT Movies.* " +
                $"  FROM Movies " +
                $"  JOIN VideoFiles ON Movies.VideoFileId = VideoFiles.VideoFileId " +
                $"  WHERE Movies.UserId = @userId" +
                $"      AND Movies.PlaybackTime IS NOT NULL" +
                $"      AND Movies.PlaybackTime < VideoFiles.Duration ";

        private string sqlUpdateMoviePlaybackTime =
                $"UPDATE Movies " +
                $"  SET PlaybackTime = @playbackTime, " +
                $"      LastViewDate = GETDATE()" +
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

        public List<Movie> GetAll(long userId)
        {
            return _context.Movies
                .Where(movie => movie.UserId == userId)
                .Include(movie => movie.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(movie => movie.VideoFile)
                .ToList();
        }

        public List<Movie> GetAllForGenre(long userId, int genreId)
        {
            var pGenreId = new SqlParameter("@genreId", genreId);
            var pUserId = new SqlParameter("@userId", userId);
            var movies = _context.Movies.FromSql(sqlSelectMoviesForGenre, pGenreId, pUserId)
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

        public List<Movie> GetMoviesInProgress(long userId)
        {
            var pUserId = new SqlParameter("@userId", userId);
            var movies = _context.Movies.FromSql(sqlSelectMoviesInProgress, pUserId)
                .Include(movie => movie.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(movie => movie.VideoFile)
                .OrderByDescending(movie => movie.LastViewDate)
                .ToList();

            return movies;

        }

        public List<Movie> GetNewlyAddedMovies(long userId)
        {
            DateTime now = DateTime.Now;
            DateTime SevenDaysAgo = now.AddDays(-7);

            return _context.Movies
                .Where(movie => movie.DateCreated > SevenDaysAgo
                             && movie.UserId == userId)
                .Include(movie => movie.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(movie => movie.VideoFile)
                .OrderByDescending(movie => movie.DateCreated)
                .ToList();
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
