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
    public class ProdusRepository: IProdusRepository
    {
        private readonly ArtizanaliiContext _context;
        public ProdusRepository(ArtizanaliiContext context)
        {
            _context = context;
        }

        public bool CreateProdus(Produs produs)
        {
            _context.Add(produs);
            return Save();
        }
        public bool CreateProdusAdmin(int id, Produs produs)
        {
            var produsUserEntity = _context.Users.Where(u => u.Id == id).FirstOrDefault();

            var numOfProduse = _context.UserProdus.Count(n => n.Id >= 0);

            var produsOwner = new UserProdus()
            {
                User = produsUserEntity,
                Produs = produs,
                Total = numOfProduse + 1
            };

            _context.Update(produsUserEntity);

            _context.Add(produsOwner);
            _context.Add(produs);
            return Save();
        }

        public Produs GetProdus(int id)
        {
            return _context.Produs.Where(p => p.Id == id).Include(prod => prod.Producator).FirstOrDefault();
        }

        public ICollection<Produs> GetProduse()
        {
            return _context.Produs.OrderBy(p => p.Id).Include(prod => prod.Producator).ToList();
        }

        public bool RemoveProdus(int id)
        {
            var produsToRemove = _context.Produs.Where(p => p.Id == id).FirstOrDefault();
            _context.Produs.Remove(produsToRemove);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProdus(Produs produs)
        {
            _context.Update(produs);
            return Save();
        }
    }
}
