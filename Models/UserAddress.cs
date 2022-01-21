using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artizanalii.Models
{
    public class UserAddress
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int Zip { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
