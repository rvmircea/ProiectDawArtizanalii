using Artizanalii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artizanalii.Interfaces
{
    public interface IProducatorRepository
    {
        ICollection<Producator> GetProducators();
        Producator GetProducator(int id);
        bool CreateProducator(Producator producator);

        bool UpdateProducator(int id, Producator producator);

        ICollection<Produs> GetProdusByProducator(int id);

        //bool PartialUpdate(User user);

        bool RemoveProducator(int id);

        bool Save();

    }
}
