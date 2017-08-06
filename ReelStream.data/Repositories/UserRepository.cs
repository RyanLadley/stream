using ReelStream.data.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using ReelStream.data.Models.Entities;
using ReelStream.data.Models.Context;
using System.Linq;

namespace ReelStream.data.Repositories
{
    public class UserRepository : IUserRepository
    {
        MainContext _context;

        public UserRepository(MainContext context)
        {
            _context = context;
        }

        public User Add(User user)
        {
            _context.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User Get(long id)
        {
            return _context.Users.FirstOrDefault(user => user.UserId == id);
        }

        public User GetFromUsername(string username)
        {
            return _context.Users.FirstOrDefault(user => user.Username == username);
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
