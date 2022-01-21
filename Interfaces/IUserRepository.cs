using Artizanalii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artizanalii.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);

        User GetUserByEmail(string email);

        bool CreateUser(User user);

        bool UpdateUser(User user);

        //bool PartialUpdate(User user);

        bool RemoveUser(int id);

        bool Save();

        


    }
}
