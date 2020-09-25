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
    public class FriendsController : ControllerBase
    {
        private IFriendService FriendService { get; set; }

        public FriendsController(IFriendService friendService)
        {
            FriendService = friendService;
        }

        // GET: api/Friends
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await FriendService.GetAll());
        }

        // GET: api/Friends/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            Friend friend = await FriendService.GetById(id);
            if (friend == null)
                return BadRequest();
            return Ok(friend);
        }

        [HttpGet("all/{id}")]
        public async Task<IActionResult> GetAllButMe(Guid id)
        {
            List<Friend> friends = await FriendService.GetAllButMe(id);
            return Ok(friends);
        }

        // POST: api/Friends
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] FriendRequest friend)
        {
            bool created = await FriendService.Add(friend);
            if (!created)
                return BadRequest();
            return Ok();
        }

        // PUT: api/Friends/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Friend friend)
        {
            bool updated = await FriendService.Update(id, friend);
            if (!updated)
                return BadRequest();
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool deleted = await FriendService.Delete(id);
            if (!deleted)
                return BadRequest();
            return Ok();
        }
    }
}
