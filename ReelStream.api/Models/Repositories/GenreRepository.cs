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

        public Genre Add(Genre movie)
        {
            throw new NotImplementedException();
        }

        public Genre Get(long id)
        {
            throw new NotImplementedException();
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
