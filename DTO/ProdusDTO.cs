using Artizanalii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artizanalii.DTO
{
    public class ProdusDTO
    {
        public string Denumire { get; set; }
        public int Price { get; set; }
        public int Rating { get; set; }
        public int An { get; set; }
        public string Descriere { get; set; }
        public int ProducatorId { get; set; }
        //public virtual Producator Producator { get; set; }
    }
}
