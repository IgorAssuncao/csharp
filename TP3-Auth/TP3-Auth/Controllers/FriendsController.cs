using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TP3_Auth.Models;
using TP3_Auth.Repository;

namespace TP3_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FriendsController : ControllerBase
    {
        private IFriendRepository FriendRepository { get; set; }
        private IConfiguration Configuration { get; set; }
        private JwtSecurityTokenHandler TokenHandler { get; set; }

        public FriendsController(IFriendRepository friendRepository, IConfiguration config)
        {
            FriendRepository = friendRepository;
            Configuration = config;
            TokenHandler = new JwtSecurityTokenHandler();
        }

        // GET: api/Friends
        [HttpGet]
        public List<Friend> Get()
        {
            return FriendRepository.FindAll();
        }

        // GET: api/Friends/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(Guid id)
        {
            Friend friend = FriendRepository.Find(id);
            if (friend == null)
                return NotFound();

            return Ok(friend);
        }

        private string GenerateToken(Guid id)
        {
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(Configuration["Token:Secret"])),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Audience = "TP3-AUTH-API",
                Issuer = "TP3-AUTH-API"
            };

            JwtSecurityToken token = TokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            TokenHandler.WriteToken(token);

            return token.RawData;
        }

        // POST: api/Friends/GetToken
        [Route("GetToken")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult GetToken([FromBody] Auth auth)
        {
            if (string.IsNullOrEmpty(auth.Email))
                return BadRequest();

            Friend friend = FriendRepository.Find(auth.Email);

            if (friend == null)
                return BadRequest();

            if (auth.Password != friend.Password)
                return BadRequest();

            string token = GenerateToken(friend.Id);



            return Ok(new { Token = token });
        }

        // POST: api/Friends
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] Friend friend)
        {
            bool result = FriendRepository.CreateFriend(friend);
            if (!result) return UnprocessableEntity();

            return Ok();
        }

        // PUT: api/Friends/5
        [HttpPut("{id}")]
        [AllowAnonymous]
        public IActionResult Put(Guid id, [FromBody] Friend friend)
        {
            bool result = FriendRepository.UpdateFriend(id, friend);
            if (!result) return UnprocessableEntity();

            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public IActionResult Delete(Guid id)
        {
            bool result = FriendRepository.DeleteFriend(id);
            if (!result) return BadRequest();

            return Ok();
        }
    }
}
