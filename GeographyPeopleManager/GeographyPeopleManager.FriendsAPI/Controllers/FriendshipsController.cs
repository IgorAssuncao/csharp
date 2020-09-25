using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;

namespace GeographyPeopleManager.FriendsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipsController : ControllerBase
    {
        private IFriendshipService FriendshipService { get; set; }

        public FriendshipsController(IFriendshipService friendshipService)
        {
            FriendshipService = friendshipService;
        }

        // GET: api/Friendships
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await FriendshipService.GetAll());
        }

        // GET: api/Friendships/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonFriends(Guid id)
        {
            var friendships = await FriendshipService.GetPersonFriends(id);
            return Ok(friendships);
        }

        // POST: api/Friendships
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Friendship friendship)
        {
            bool created = await FriendshipService.Add(friendship);
            if (!created)
                return BadRequest();
            return Ok();
        }

        // PUT: api/Friendships/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, [FromQuery] Guid FriendId)
        {
            var friendship = new Friendship() { PersonId = id, FriendId = FriendId };
            bool deleted = await FriendshipService.Delete(friendship);
            if (!deleted)
                return BadRequest();
            return Ok();
        }
    }
}
