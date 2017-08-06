using ReelStream.data.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReelStream.data.Models.Entities;
using ReelStream.data.Models.Context;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ReelStream.data.Models.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private MainContext _context;

        #region SQL
        string sqlGenresByUser =
                "SELECT DISTINCT Genres.* " +
                "    FROM Genres " +
                "    JOIN MovieGenres ON MovieGenres.GenreId = Genres.GenreId " +
                "    JOIN Movies ON Movies.MovieId = MovieGenres.MovieId " +
                "    WHERE Movies.UserId = @userId ";
        #endregion

        public GenreRepository(MainContext context)
        {
            _context = context;
        }

        
        public Genre Add(Genre genre)
        {
            throw new NotImplementedException();
        }

        public Genre GetFromId(int id)
        {
            return _context.Genres.Where(genre => genre.GenreId == id).FirstOrDefault();
        }

        public List<Genre> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Genre> GetByExternalIds(ICollection<int> genreIds)
        {
            var genres = (from genre in _context.Genres
                          where genreIds.Contains(genre.ExternalId.Value)
                          select genre).ToList();

            return genres;
                         
        }

        /// <summary>
        /// Get all genres the provided user has used in their movie collection
        /// </summary>
        /// <param name="genreIds"></param>
        /// <returns></returns>
        public List<Genre> GetByUser(long userId)
        {
            var pUserId = new SqlParameter("@userId", userId);
            var genres = _context.Genres.FromSql(sqlGenresByUser, pUserId).ToList();

            return genres;

        }

        public void Remove(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(long id)
        {
            throw new NotImplementedException();
        }
    }
}
