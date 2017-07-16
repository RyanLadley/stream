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
        private string sqlMoviesForGenre =
                $"SELECT Movies.*"+
                $"  FROM Movies" +
                $"  JOIN MovieGenres on MovieGenres.MovieId = Movies.MovieId" +
                $"  WHERE MovieGenres.GenreId = @genreId";
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

        public Movie Get(long id)
        {
            throw new NotImplementedException();
        }

        public List<Movie> GetAll()
        {
            return _context.Movies
                .Include(movie => movie.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .ToList();
        }

        public List<Movie> GetAllForGenre(int genreId)
        {
            var pGenreID = new SqlParameter("genreId", genreId);
            var movies = _context.Movies.FromSql(sqlMoviesForGenre, pGenreID)
                .Include(movie => movie.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .ToList();

            return movies;
        }

        public void Remove(long id)
        {
            throw new NotImplementedException();
        }

        public Movie Update(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();

            return movie;
        }
    }
}
