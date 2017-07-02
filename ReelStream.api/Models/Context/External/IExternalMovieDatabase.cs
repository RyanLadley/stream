using ReelStream.api.Models.DataTransfer;
using ReelStream.api.Models.DataTransfer.External;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.api.Models.Context.External
{
    public interface IExternalMovieDatabase
    {
        Task<List<ExternalMovie>> SearchMovie(string title);
        Task SaveMovieImage(NewMovieForm movie);
    }
}
