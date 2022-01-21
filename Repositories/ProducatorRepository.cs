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
    public class ProducatorRepository : IProducatorRepository
    {
        private readonly ArtizanaliiContext _context;
        public ProducatorRepository(ArtizanaliiContext context)
        {
            _context = context;
        }
        public bool CreateProducator(Producator producator)
        {
            _context.Add(producator);
            return Save();
        }

        public Producator GetProducator(int id)
        {
            return _context.Producators.Where(p => p.Id == id).Include(p => p.Produs).FirstOrDefault();
        }

        public ICollection<Produs> GetProdusByProducator(int id)
        {
            return _context.Produs.Where(p => p.ProducatorId == id).ToList();
        }

        public ICollection<Producator> GetProducators()
        {
            return _context.Producators.OrderBy(p => p.Id).Include(p=>p.Produs).ToList();
        }

        public bool RemoveProducator(int id)
        {
            var producatorToRemove = _context.Producators.Where(p => p.Id == id).FirstOrDefault();
            _context.Producators.Remove(producatorToRemove);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProducator(int producatorId, Producator producator)
        {
            _context.Update(producator);
            return Save();
        }
    }
}
