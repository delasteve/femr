using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FEMR.Commands;
using FEMR.Core;
using FEMR.Queries;
using FEMR.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FEMR.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ICommandProcessor _commandProcessor;
        private readonly IQueryProcessor _queryProcessor;

        public UsersController(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor)
        {
            _commandProcessor = commandProcessor;
            _queryProcessor = queryProcessor;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { };
        }

        [HttpGet("{userId}", Name = "GetUser")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var user = await _queryProcessor.Process(new GetUser(userId));

            return new JsonResult(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserPostModel userPostModel)
        {
            var userId = Guid.NewGuid();
            await _commandProcessor.Process(new CreateUser(userId, userPostModel.Email, userPostModel.Password, userPostModel.FirstName, userPostModel.LastName));
            var user = await _queryProcessor.Process(new GetUser(userId));

            return CreatedAtRoute("GetUser", new { userId = userId }, user);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
