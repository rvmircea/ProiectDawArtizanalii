using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artizanalii.Models
{
    public class UserProdus
    {
        public int Id { get; set; }
        public int Total { get; set; }
        public int UserId { get; set; }
        public int ProdusId { get; set; }
        public User User { get; set; }
        public Produs Produs { get; set; }

    }
}
