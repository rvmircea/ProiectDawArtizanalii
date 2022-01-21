using Artizanalii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artizanalii.Interfaces
{
    public interface IProdusRepository
    {
        ICollection<Produs> GetProduse();
        
        Produs GetProdus(int id);

        bool CreateProdusAdmin(int id, Produs produs);

        bool CreateProdus(Produs produs);
        bool UpdateProdus(Produs produs);

        //bool PartialUpdate(User user);

        bool RemoveProdus(int id);

        bool Save();

    }
}
