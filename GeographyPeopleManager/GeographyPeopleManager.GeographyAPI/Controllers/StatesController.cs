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
    public class StatesController : ControllerBase
    {
        private IStateService StateService { get; set; }

        public StatesController(IStateService stateService)
        {
            StateService = stateService;
        }

        // GET: api/States
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await StateService.GetAll());
        }

        // GET: api/States/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            State state = await StateService.GetById(id);
            if (state == null)
                return BadRequest();
            return Ok(state);
        }

        // POST: api/States
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] StateRequest state)
        {
            bool created = await StateService.Add(state);
            if (!created)
                return BadRequest();
            return Ok();
        }

        // PUT: api/States/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] State state)
        {
            bool updated = await StateService.Update(id, state);
            if (!updated)
                return BadRequest();
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool deleted = await StateService.Delete(id);
            if (!deleted)
                return BadRequest();
            return Ok();
        }
    }
}
