using Artizanalii.DTO;
using Artizanalii.Helpers;
using Artizanalii.Interfaces;
using Artizanalii.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artizanalii.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;
        public UsersController(IUserRepository userRepository, JwtService jwtService )
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }
        
        [HttpGet]
        public IActionResult GetUsers()
        {
            var newUsers = from u in _userRepository.GetUsers()
                           select new UserDTO
                           {
                               FirstName = u.FirstName,
                               LastName = u.LastName,
                               Email = u.Email
                           };

            //var users = _userRepository.GetUsers();
            return Ok(newUsers);
        }
        [HttpGet("all")]
        public IActionResult GetUsersWithFullDetails()
        {
            //var newUsers = from u in _userRepository.GetUsers()
            //               select new UserDTO
            //               {
            //                   FirstName = u.FirstName,
            //                   LastName = u.LastName,
            //                   Email = u.Email
            //               };

            var users = _userRepository.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userRepository.GetUser(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("log")]
        public IActionResult LogUser()
        {
            try
            {
                var jwt = Request.Headers["Authorization"];

                var token = _jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);

                var userToLogIn = _userRepository.GetUser(userId);

                return Ok(userToLogIn);
            }
            catch(Exception _)
            {
                return Unauthorized();
            }
           
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new
            {
                message = "Logged out !"
            });
        }


        [HttpPost("login")]
        public IActionResult Login(LoggedUserDTO loggedUserDTO)
        {
            var userToLogin = _userRepository.GetUserByEmail(loggedUserDTO.Email);
            if(userToLogin == null)
            {
                return BadRequest(new { message = "Invalid credentials" });
            }
            if(!BCrypt.Net.BCrypt.Verify(loggedUserDTO.Password, userToLogin.Password))
            {
                return BadRequest(new { message = "Invalid credentials" });
            }


            var jwt = _jwtService.Generate(userToLogin.Id);

            //Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true });
            return Ok(new
            {
                jwt = jwt
            });

            //return Ok(userToLogin);
            //return Ok(new
            //{
            //    message = "Succes !" 
            //});
        }

        [HttpPost("create")]
        public IActionResult CreateUser(UserDTO user)
        {
            //var userToCreate = _userRepository.CreateUser(user);
            var userToCreate = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
            };
            _userRepository.CreateUser(userToCreate);
            return Ok("User created succesfuly");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser([FromBody] User userToUpdate)
        {
            
           var rezult = _userRepository.UpdateUser(userToUpdate);
            return Ok(rezult);
        }

        //[HttpPatch("{id}")]
        //public IActionResult PartialUpdate(int id, [FromBody] JsonPatchDocument<User> userToPatch)
        //{
        //    if(userToPatch == null)
        //    {
        //        return BadRequest();
        //    }
        //    else
        //    {
        //        var userToPartialUpdate = _userRepository.GetUser(id);
        //        patchDocument.ApplyTo(userToPartialUpdate, ModelState)
        //    }
        //}

        
        [HttpDelete("{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            _userRepository.RemoveUser(id);
            return Ok("Deleted succesful");
        }
    }
}
