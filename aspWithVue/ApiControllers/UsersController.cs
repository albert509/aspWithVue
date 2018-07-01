using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspWithVue.Data;
using aspWithVue.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aspWithVue.ApiControllers
{
    [Produces("application/json")]
    //[Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly AppDbContext _dbContext;
        public UsersController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("api/saveAll")]
        public async Task<IActionResult> SaveAllUsers([FromBody]List<User> users)
        {
            await _dbContext.Users.AddRangeAsync(users);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("api/users")]
        public IActionResult GetAllUsers()
        {
            var users = _dbContext.Users.ToList();
            return new JsonResult(users);
        }

        [HttpDelete("api/delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == id);
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("api/addUser")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return new JsonResult(user);
        }

    }
}