using Artizanalii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artizanalii.Interfaces
{
    public interface IUserAddressRepository
    {
        ICollection<UserAddress> GetUserAddresses();
        UserAddress GetUserAddress(int id);

        User GetUserByAddress(int id);

        bool UpdateUserAddress(UserAddress userAddress);
        bool RemoveUserAddress(int id);
        bool CreateUserAddress(UserAddress userAddress);

        bool Save();
    
    }
}
