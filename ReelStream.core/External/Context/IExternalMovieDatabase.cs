using ReelStream.core.Models.DataTransfer.Form;
using ReelStream.core.External.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.core.External.Context
{
    public interface IExternalMovieDatabase
    {
        Task<List<ExternalMovie>> SearchMovie(string title);
        Task SaveMovieImage(NewMovieForm movie);
    }
}
