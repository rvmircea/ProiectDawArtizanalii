using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artizanalii.Models
{
    public class Producator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public virtual ICollection<Produs> Produs { get; set; }

    }
}
