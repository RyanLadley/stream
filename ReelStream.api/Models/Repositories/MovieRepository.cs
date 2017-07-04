using System;
using System.Collections.Generic;
using ReelStream.api.Models.Entities;
using ReelStream.api.Models.Repositories.IRepositories;
using ReelStream.api.Models.Context;
using System.Linq;

namespace ReelStream.api.Models.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private MainContext _context;

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
            return _context.Movies.ToList();
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
