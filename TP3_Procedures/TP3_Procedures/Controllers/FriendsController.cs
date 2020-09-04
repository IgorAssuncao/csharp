using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TP3_Procedures.Data;
using TP3_Procedures.Models;

namespace TP3_Procedures.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        public ITP3_ProceduresContext _context { get; set; }

        public FriendsController(ITP3_ProceduresContext context)
        {
            _context = context;
        }

        // GET: api/Friends
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var friends = await _context.ExecuteSqlSelectManyFriendsAsync("exec SelectAllFriends");

            return Ok(friends);
        }

        // GET: api/Friends/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var friend = await _context.ExecuteSqlSelectOneFriendAsync($"exec SelectFriend {id}");

            if (friend == null)
                return BadRequest();

            return Ok(friend);
        }

        // POST: api/Friends
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Friend friend)
        {
            var result = await _context.ExecuteSqlInsertFriendAsync($"exec InsertFriend {Guid.NewGuid()}, {friend.Name}, {friend.Lastname}, {friend.Email}, {friend.Birthday}");

            if (result == 0)
                return BadRequest();
            
            return CreatedAtAction("Post", new object());
        }

        // PUT: api/Friends/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] Friend updatedFriend)
        {
            var friend = await _context.ExecuteSqlSelectOneFriendAsync($"exec SelectFriend {id}");

            if (friend == null)
                return BadRequest();

            var name = updatedFriend.Name ?? friend.Name;
            var lastname = updatedFriend.Lastname ?? friend.Lastname;
            var email = updatedFriend.Email ?? friend.Email;
            var birthday = updatedFriend.Birthday.CompareTo(new DateTime(0001, 1, 1)) == 0 ? friend.Birthday : updatedFriend.Birthday;

            await _context.ExecuteSqlUpdateFriendAsync($"exec UpdateFriend {id}, {name}, {lastname}, {email}, {birthday}");
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var friend = await _context.ExecuteSqlSelectOneFriendAsync($"exec SelectFriend {id}");

            if (friend == null)
                return BadRequest();

            await _context.ExecuteSqlDeleteFriendAsync($"exec DeleteFriend {id}");
            return NoContent();
        }
    }
}
