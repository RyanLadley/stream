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
            _context.Add(movie);
            _context.SaveChanges();

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
