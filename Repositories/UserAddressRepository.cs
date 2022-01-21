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
    public class UserAddressRepository : IUserAddressRepository
    {
        private readonly ArtizanaliiContext _context;
        public UserAddressRepository(ArtizanaliiContext context)
        {
            _context = context;
        }
        public bool CreateUserAddress(UserAddress userAddress)
        {
            _context.Add(userAddress);
            return Save();

        }

        public UserAddress GetUserAddress(int id)
        {
            return _context.UsersAddresses.Where(p => p.Id == id).Include(p => p.User).FirstOrDefault();
        }

        public ICollection<UserAddress> GetUserAddresses()
        {
            return _context.UsersAddresses.OrderBy(u => u.Id).ToList();
        }

        public User GetUserByAddress(int id)
        {
            return _context.UsersAddresses.Where(u => u.UserId == id).Select(ua => ua.User).FirstOrDefault();
        }

        public bool RemoveUserAddress(int id)
        {
            var userAddressToRemove = _context.UsersAddresses.Where(p => p.Id == id).FirstOrDefault();
            _context.UsersAddresses.Remove(userAddressToRemove);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUserAddress(UserAddress userAddress)
        {
            _context.Update(userAddress);
            return Save();
        }
    }
}
