using Artizanalii.Data;
using Artizanalii.Interfaces;
using Artizanalii.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artizanalii.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ArtizanaliiContext _context;
        public UserRepository(ArtizanaliiContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            
            _context.Add(user);
            return Save();
            
        }

        public User GetUser(int id)
        {
            
            return _context.Users.Where(p => p.Id == id).FirstOrDefault();
            //return _context.Users.Find(id);
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(p => p.Id).Include(u => u.UserAddress).Include(u => u.UserProdus).ToList();
        }
        public bool UpdateUser(User user)
        {

            _context.Update(user);
            return Save();
        }

        //public bool PartialUpdate(User user)
        //{
        //    _context.Update(user);
        //    return Save();
        //}

        public bool RemoveUser(int id)
        {
            var userToRemove = _context.Users.Where(p => p.Id == id).FirstOrDefault();
            _context.Users.Remove(userToRemove);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.Where(u => u.Email == email).FirstOrDefault();
        }
    }
}
