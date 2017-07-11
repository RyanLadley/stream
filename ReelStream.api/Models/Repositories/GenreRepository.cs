using ReelStream.api.Models.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReelStream.api.Models.Entities;
using ReelStream.api.Models.Context;

namespace ReelStream.api.Models.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private MainContext _context;

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
                          where genreIds.Contains(genre.ExternalId)
                          select genre).ToList();

            return genres;
                         
        }

        /// <summary>
        /// Get all genres the provided user has used in their movie collection
        /// </summary>
        /// <param name="genreIds"></param>
        /// <returns></returns>
        public List<Genre> GetImplementedByUser()
        {
            var genres = (from genre in _context.Genres
                          where 
                          (
                            from movieGenre in _context.MovieGenres
                            select movieGenre.GenreId
                          ).Contains(genre.GenreId)
                          select genre).ToList();

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
