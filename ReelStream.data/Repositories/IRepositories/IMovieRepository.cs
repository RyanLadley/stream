using ReelStream.data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.data.Repositories.IRepositories
{
    public interface IMovieRepository
    {
        Movie Add(Movie movie);
        Movie Get(long movieId);
        List<Movie> GetAll(long userId);
        void Remove(long id);
        Movie UpdatePlayback(Movie movie);
        List<Movie> GetAllForGenre(long userId, int genreId);
        List<Movie> GetMoviesInProgress(long userId);
        List<Movie> GetNewlyAddedMovies(long userId);
    }
}
