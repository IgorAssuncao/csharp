using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Mappings;
using Biblioteca.Models;
using Biblioteca.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BookService BookService { get; set; }

        public BooksController(BookService bookService)
        {
            BookService = bookService;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await BookService.GetAll());
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            BookMapping book = await BookService.GetById(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        // POST: api/Books
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookMapping bookMapping)
        {
            bool created = await BookService.Add(bookMapping.Title, bookMapping.ISBN, bookMapping.Year, bookMapping.Authors);

            if (!created)
                return BadRequest();

            return Ok();
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Book book)
        {
            bool updated = await BookService.Update(id, book.Title, book.ISBN, book.Year);

            if (!updated)
                return BadRequest();

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool deleted = await BookService.Remove(id);

            if (!deleted)
                return BadRequest();

            return NoContent();
        }
    }
}
