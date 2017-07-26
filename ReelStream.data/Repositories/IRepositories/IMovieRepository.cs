using ReelStream.data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.data.Models.Repositories.IRepositories
{
    public interface IMovieRepository
    {
        Movie Add(Movie movie);
        Movie Get(long movieId);
        List<Movie> GetAll();
        void Remove(long id);
        Movie UpdatePlayback(Movie movie);
        List<Movie> GetAllForGenre(int genreId);
    }
}
