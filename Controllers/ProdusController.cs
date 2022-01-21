using Artizanalii.DTO;
using Artizanalii.Interfaces;
using Artizanalii.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artizanalii.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdusController : ControllerBase
    {
        private readonly IProdusRepository _produsRepository;
        private readonly IProducatorRepository _producatorRepository;
        public ProdusController(IProdusRepository produsRepository,
                                IProducatorRepository producatorRepository)
        {
            _produsRepository = produsRepository;
            _producatorRepository = producatorRepository;
        }

        [HttpGet]
        public IActionResult GetProduse()
        {
            var newProduse = from p in _produsRepository.GetProduse()
                             select new ProdusDTO
                             {
                                 Denumire = p.Denumire,
                                 Rating = p.Rating,
                                 Price = p.Price,
                                 An = p.An,
                                 Descriere = p.Descriere,
                                 ProducatorId = p.ProducatorId
                           };

            //var users = _userRepository.GetUsers();
            return Ok(newProduse);
        }

        [HttpGet("all")]
        public IActionResult GetProdusWithFullInfo()
        {
            //var newUsers = from u in _userRepository.GetUsers()
            //               select new UserDTO
            //               {
            //                   FirstName = u.FirstName,
            //                   LastName = u.LastName,
            //                   Email = u.Email
            //               };

            var produse = _produsRepository.GetProduse();
            return Ok(produse);
        }


        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var produs = _produsRepository.GetProdus(id);
            if (produs == null)
            {
                return NotFound();
            }
            return Ok(produs);
        }

        [HttpPost("admin/create")]
        public IActionResult CreateProdusAdmin([FromQuery] int userId, ProdusDTO produs)
        {
            //var userToCreate = _userRepository.CreateUser(user);
            var produsToCreate = new Produs
            {
                Denumire = produs.Denumire,
                Rating = produs.Rating,
                Price = produs.Price,
                An = produs.An,
                Descriere = produs.Descriere,
                ProducatorId = produs.ProducatorId
            };
            _produsRepository.CreateProdusAdmin(userId, produsToCreate);
            _producatorRepository.UpdateProducator(produsToCreate.ProducatorId, _producatorRepository.GetProducator(produs.ProducatorId));
            return Ok(produsToCreate);
        }

        [HttpPost("create")]
        public IActionResult CreateProdus(ProdusDTO produs)
        {
            //var userToCreate = _userRepository.CreateUser(user);
            var produsToCreate = new Produs
            {
                Denumire = produs.Denumire,
                Rating = produs.Rating,
                Price = produs.Price,
                An = produs.An,
                Descriere = produs.Descriere,
                ProducatorId = produs.ProducatorId
            };
            _produsRepository.CreateProdus(produsToCreate);
            _producatorRepository.UpdateProducator(produsToCreate.ProducatorId ,_producatorRepository.GetProducator(produs.ProducatorId));
            return Ok(produsToCreate);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProdus(int id, [FromBody] Produs produsToUpdate)
        {
            if (id != produsToUpdate.Id)
            {
                return NotFound();
            }
            var rezult = _produsRepository.UpdateProdus(produsToUpdate);
            return Ok(rezult);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProdus([FromRoute] int id)
        {
            if (_produsRepository.GetProdus(id) == null)
            {
                return NotFound();
            }
            _produsRepository.RemoveProdus(id);
            return Ok("Deleted succesful");
        }
    }
}
