using ReelStream.data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelStream.data.Repositories.IRepositories
{
    public interface IUserRepository
    {
        User Add(User user);
        User Get(long id);
        void Remove(long id);
        void Update(long id);
        User GetFromUsername(string Username);
    }
}
