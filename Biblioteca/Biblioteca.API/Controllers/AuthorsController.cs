using System;
using System.Threading.Tasks;
using Biblioteca.Mappings;
using Biblioteca.Models;
using Biblioteca.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private AuthorService AuthorService { get; set; }

        public AuthorsController(AuthorService authorService)
        {
            AuthorService = authorService;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await AuthorService.GetAll());
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            AuthorMapping author = await AuthorService.GetById(id);

            if (author == null)
                return NotFound();

            return Ok(author);
        }

        // POST: api/Authors
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Author author)
        {
            bool created = await AuthorService.Add(author.Name, author.Lastname, author.Email, author.Password, author.Birthday);

            if (!created)
                return BadRequest();
            return Ok();
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult>Put(Guid id, [FromBody] Author author)
        {
            bool updated = await AuthorService.Update(id, author.Name, author.Lastname, author.Email, author.Password, author.Birthday);

            if (!updated)
                return BadRequest();

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool deleted = await AuthorService.Remove(id);

            if (!deleted)
                return BadRequest();

            return NoContent();
        }
    }
}
