using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;

namespace GeographyPeopleManager.GeographyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private ICountryService CountryService { get; set; }

        public CountriesController(ICountryService countryService)
        {
            CountryService = countryService;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await CountryService.GetAll());
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            Country country = await CountryService.GetById(id);
            if (country == null)
                return BadRequest();
            return Ok(country);
        }

        // POST: api/Countries
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CountryRequest country)
        {
            bool created = await CountryService.Add(country);
            if (!created)
                return BadRequest();
            return Ok();
        }

        // PUT: api/Countries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Country country)
        {
            bool updated = await CountryService.Update(id, country);
            if (!updated)
                return BadRequest();
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool deleted = await CountryService.Delete(id);
            if (!deleted)
                return BadRequest();
            return Ok();
        }
    }
}
