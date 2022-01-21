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
    public class ProducatorController : ControllerBase
    {
        private readonly IProducatorRepository _producatorRepository;
        public ProducatorController(IProducatorRepository producatorRepository)
        {
            _producatorRepository = producatorRepository;
        }

        [HttpGet]
        public IActionResult GetProducators()
        {
            var newProducators = from u in _producatorRepository.GetProducators()
                                 select new ProducatorDTO
                                 {
                                     Name = u.Name,
                                     City = u.City,
                                 };

            //var users = _userRepository.GetUsers();
            return Ok(newProducators);
        }

        [HttpGet("all")]
        public IActionResult GetProducatorsWithFullDetails()
        {
            //var newUsers = from u in _userRepository.GetUsers()
            //               select new UserDTO
            //               {
            //                   FirstName = u.FirstName,
            //                   LastName = u.LastName,
            //                   Email = u.Email
            //               };

            var producators = _producatorRepository.GetProducators();
            return Ok(producators);
        }

        [HttpGet("{id}")]
        public IActionResult GetProducator(int id)
        {
            var prod = _producatorRepository.GetProducator(id);
            if (prod == null)
            {
                return NotFound();
            }
            return Ok(prod);
        }
        [HttpGet("{id}/produse")]
        public IActionResult GetProdusByProducator(int id)
        {
            var prod = _producatorRepository.GetProdusByProducator(id);
                       
            if (prod == null)
            {
                return BadRequest();

            }
            return Ok(prod);
        }


        [HttpPost("create")]
        public IActionResult CreateProducator(Producator prod)
        {
            //var userToCreate = _userRepository.CreateUser(user);
            var prodToCreate = new Producator
            {
                Name = prod.Name,
                City = prod.City,
                Produs = prod.Produs
            };
            _producatorRepository.CreateProducator(prodToCreate);
            return Ok("Producator created succesfuly");
        }


        [HttpPut("{prodId}")]
        public IActionResult UpdateProducator([FromQuery] int prodId, [FromBody] Producator producatorToUpdate)
        {
            if (prodId != producatorToUpdate.Id)
            {
                return BadRequest();
            }
            var rezult = _producatorRepository.UpdateProducator(prodId, producatorToUpdate);
            return Ok(rezult);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            _producatorRepository.RemoveProducator(id);
            return Ok("Deleted succesful");
        }
    }
}
