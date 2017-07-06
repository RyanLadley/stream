using ReelStream.api.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.api.Models.Repositories.IRepositories
{
    public interface IGenreRepository
    {
        Genre Add(Genre movie);
        Genre Get(long id);
        List<Genre> GetAll();
        List<Genre> GetByExternalIds(ICollection<int> genreIds);
        void Remove(long id);
        void Update(long id);
    }
}
