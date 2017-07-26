using ReelStream.data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.data.Models.Repositories.IRepositories
{
    public interface IGenreRepository
    {
        Genre Add(Genre genre);
        Genre GetFromId(int id);
        List<Genre> GetAll();
        List<Genre> GetByExternalIds(ICollection<int> genreIds);
        List<Genre> GetImplementedByUser();
        void Remove(long id);
        void Update(long id);
    }
}
