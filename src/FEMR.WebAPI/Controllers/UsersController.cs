using System;
using System.Threading.Tasks;
using FEMR.Commands;
using FEMR.Core;
using FEMR.Queries;
using FEMR.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{userId}", Name = "GetUser")]
        [Authorize(Policy = "Users")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var user = await _queryProcessor.Process(new GetUserInfo(userId));

            return new JsonResult(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserPostModel userPostModel)
        {
            var userId = Guid.NewGuid();
            await _commandProcessor.Process(new CreateUser(userId, userPostModel.Email, userPostModel.Password, userPostModel.FirstName, userPostModel.LastName));
            var user = await _queryProcessor.Process(new GetUserInfo(userId));

            return CreatedAtRoute("GetUser", new { userId = userId }, user);
        }
    }
}
