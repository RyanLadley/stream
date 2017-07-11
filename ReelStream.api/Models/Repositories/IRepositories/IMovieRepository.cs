using ReelStream.api.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.api.Models.Repositories.IRepositories
{
    public interface IMovieRepository
    {
        Movie Add(Movie movie);
        Movie Get(long id);
        List<Movie> GetAll();
        void Remove(long id);
        Movie Update(Movie movie);
        List<Movie> GetAllForGenre(int genreId);
    }
}
